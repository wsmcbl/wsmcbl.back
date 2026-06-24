using ClosedXML.Excel;
using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;
using static wsmcbl.src.controller.service.sheet.SpreadSheetMaker;

namespace wsmcbl.src.controller.service.sheet;

public class EnrollmentMultiGradeSummarySheetBuilder : SheetBuilder
{
    private List<PartialReportData> partialsData { get; set; } = null!;
    private string schoolyear { get; set; } = null!;
    private string enrollmentLabel { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;

    // Estado dinámico que cambia por cada pestaña que se procesa
    private PartialEntity currentPartial { get; set; } = null!;
    private List<model.secretary.SubjectEntity> currentSubjectList { get; set; } = null!;
    private List<string> orderedSubjectIdList { get; set; } = [];

    public EnrollmentMultiGradeSummarySheetBuilder(
        string schoolyear,
        TeacherEntity teacher,
        string userAlias,
        string enrollmentLabel,
        List<PartialReportData> partialsData)
    {
        this.schoolyear = schoolyear;
        this.teacher = teacher;
        this.userAlias = userAlias;
        this.enrollmentLabel = enrollmentLabel;
        this.partialsData = partialsData;
    }

    public override byte[] getSpreadSheet()
    {
        using var workbook = new XLWorkbook();

        // 1. Generamos primero la lista unificada y ordenada de TODOS los alumnos del semestre
        var allStudents = partialsData
            .SelectMany(p => p.StudentList)
            .GroupBy(s => s.studentId)
            .Select(g => g.First())
            .OrderBy(s => s.student.sex)
            .ThenBy(s => s.fullName())
            .ToList();

        // 2. Generar las hojas de cada parcial individual de forma dinámica pero HOMOGÉNEA
        foreach (var data in partialsData)
        {
            currentPartial = data.Partial;
            currentSubjectList = data.SubjectList
                .Select(e => e.secretarySubject!)
                .OrderBy(s => s.areaId)
                .ThenBy(s => s.number)
                .ToList();
            
            // --- FRAGMENTO PARA DEBUGEAR EN CONSOLE ---
            Console.WriteLine("\n===============================================================================");
            Console.WriteLine($"DEBUG: Orden de Asignaturas para la hoja [{currentPartial?.label ?? "Resumen"}]");
            Console.WriteLine("===============================================================================");
            foreach (var subject in currentSubjectList)
            {
                Console.WriteLine($"[Area: {subject.areaId}] [Num: {subject.number}] -> ID: {subject.subjectId,-10} | Initials: {subject.initials,-6} | Name: {subject.name}");
            }
            Console.WriteLine("===============================================================================\n");
            // ------------------------------------------
            
            
            
            setColumnQuantityMulti(currentSubjectList.Count);
            orderedSubjectIdList = currentSubjectList.Select(e => e.subjectId).ToList()!;

            initWorksheet(workbook, currentPartial!.label);

            // PASAMOS la lista completa de alumnos en lugar de data.StudentList
            buildSingleSheet(allStudents);
        }

        // 3. Generar la hoja definitiva de Resumen
        buildSummarySheet(workbook);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
    }

    private void buildSingleSheet(List<StudentEntity> studentList)
    {
        setTitleMulti(currentPartial.label);
        setDate(6, userAlias);
        const int headerRow = 9;
        setHeaderMulti(headerRow, currentSubjectList);

        var counter = headerRow + 1;
        foreach (var item in studentList)
        {
            setBodyMulti(counter, item, counter - headerRow, currentPartial.partialId);
            counter++;
        }

        var lastRow = studentList.Count + headerRow;
        setBorder(lastRow, headerRow);
        worksheet.Columns().AdjustToContents();
        setWithMulti(headerRow, lastRow);

        worksheet.SheetView.FreezeRows(headerRow);
    }

    private void buildSummarySheet(XLWorkbook workbook)
    { 
        var allStudents = partialsData
            .SelectMany(p => p.StudentList)
            .GroupBy(s => s.studentId)
            .Select(g => g.First())
            .OrderBy(s => s.student.sex) 
            .ThenBy(s => s.fullName())
            .ToList();

        // Tomamos el primer parcial solo como molde base de la estructura de asignaturas
        var templateData = partialsData.First();
        currentSubjectList = templateData.SubjectList
            .Select(e => e.secretarySubject!)
            .OrderBy(s => s.areaId)   // Primero agrupa por el ID del Área Académica
            .ThenBy(s => s.number)    // Luego las ordena por su número de posición oficial (1, 2, 3...)
            .ToList();        setColumnQuantityMulti(currentSubjectList.Count);
        orderedSubjectIdList = currentSubjectList.Select(e => e.subjectId).ToList()!;
        
        initWorksheet(workbook, $"{currentPartial.semester} Semestre");

        setTitleMulti($"{currentPartial.semester} Semestre");
        setDate(6, userAlias);

        const int headerRow = 9;
        setHeaderMulti(headerRow, currentSubjectList);

        var counter = headerRow + 1;

        foreach (var studentTemplate in allStudents)
        {
            var bodyColumn = 2;
            var pos = counter - headerRow;

            worksheet.Cell(counter, bodyColumn++).Value = pos;
            worksheet.Cell(counter, bodyColumn++).Value = studentTemplate.studentId;
            worksheet.Cell(counter, bodyColumn++).Value = studentTemplate.student?.minedId?.getOrDefault() ?? "";
            worksheet.Cell(counter, bodyColumn++).Value = studentTemplate.fullName();
            worksheet.Cell(counter, bodyColumn++).Value = (studentTemplate.student?.sex ?? true) ? "M" : "F";

            decimal totalConductSum = 0;
            decimal totalGradeSum = 0;

            int partialsWithConductCount = 0;
            int partialsWithGradeCount = 0;

            // Calcular el promedio por asignatura de forma cruzada y segura
            foreach (var subjectId in orderedSubjectIdList)
            {
                decimal totalSubjectGrade = 0;
                string currentLabel = "";
                int subjectPartialCount = 0;

                foreach (var pData in partialsData)
                {
                    var studentInPartial =
                        pData.StudentList.FirstOrDefault(s => s.studentId == studentTemplate.studentId);
                    if (studentInPartial == null) continue; // Si el alumno no existía en este parcial, saltar

                    var gradeRecord = studentInPartial.gradeList?.FirstOrDefault(g => g.subjectId == subjectId);
                    if (gradeRecord != null)
                    {
                        // CORRECCIÓN: grade no es null, se suma directo
                        totalSubjectGrade += gradeRecord.grade;
                        currentLabel = gradeRecord.label ?? currentLabel;
                        subjectPartialCount++;
                    }
                }

                // Evitamos división por cero si nunca llevó la materia o el alumno no estuvo en los parciales
                decimal finalSubjectAverage = subjectPartialCount > 0 ? (totalSubjectGrade / subjectPartialCount) : 0;

                worksheet.Cell(counter, bodyColumn).Value = currentLabel;
                worksheet.Cell(counter, bodyColumn + 1).Value = finalSubjectAverage;

                if (finalSubjectAverage < 60 && subjectPartialCount > 0)
                {
                    worksheet.Cell(counter, bodyColumn + 1).Style.Fill.BackgroundColor = redColor;
                }

                bodyColumn += 2;
            }

            // Calcular promedios generales cuantitativos de Conducta y Nota Final por parcial activo
            foreach (var pData in partialsData)
            {
                var studentInPartial = pData.StudentList.FirstOrDefault(s => s.studentId == studentTemplate.studentId);
                if (studentInPartial == null) continue;

                var average = studentInPartial.getAverage(pData.Partial.partialId);
                if (average != null)
                {
                    // CORRECCIÓN: Como no son nullables, sumamos directamente y contamos el parcial activo
                    totalConductSum += average.conductGrade;
                    partialsWithConductCount++;

                    totalGradeSum += average.grade;
                    partialsWithGradeCount++;
                }
            }

            // Divisiones seguras controlando traslados/retiros
            decimal conductAverage = partialsWithConductCount > 0 ? (totalConductSum / partialsWithConductCount) : 0;
            decimal finalGradeAverage = partialsWithGradeCount > 0 ? (totalGradeSum / partialsWithGradeCount) : 0;

            worksheet.Cell(counter, bodyColumn++).Value = partialsWithConductCount > 0
                ? getQualitativeConductLabel((double)conductAverage)
                : "-";
            worksheet.Cell(counter, bodyColumn++).Value =
                partialsWithConductCount > 0 ? conductAverage.ToString("F2") : "-";
            worksheet.Cell(counter, bodyColumn).Value =
                partialsWithGradeCount > 0 ? finalGradeAverage.ToString("F2") : "-";

            counter++;
        }

        var lastRow = allStudents.Count + headerRow;
        setBorder(lastRow, headerRow);
        worksheet.Columns().AdjustToContents();
        setWithMulti(headerRow, lastRow);
        worksheet.SheetView.FreezeRows(headerRow);
    }

    // --- REUTILIZACIÓN DE MÉTODOS ADAPTADOS DE TU CLASE BASE ---

    private void setBodyMulti(int headerRow, StudentEntity student, int pos, int partialId)
    {
        var bodyColumn = 2;
        worksheet.Cell(headerRow, bodyColumn++).Value = pos;
        worksheet.Cell(headerRow, bodyColumn++).Value = student.studentId;
        worksheet.Cell(headerRow, bodyColumn++).Value = student.student?.minedId?.getOrDefault() ?? "";
        worksheet.Cell(headerRow, bodyColumn++).Value = student.fullName();
        worksheet.Cell(headerRow, bodyColumn++).Value = (student.student?.sex ?? true) ? "M" : "F";

        // BUSCAMOS si el estudiante realmente tiene notas en ESTE parcial actual
        var currentPartialData = partialsData.FirstOrDefault(p => p.Partial.partialId == partialId);
        var realStudentInPartial =
            currentPartialData?.StudentList?.FirstOrDefault(s => s.studentId == student.studentId);

        // SI NO TIENE REGISTRO EN ESTE PARCIAL (Traslado/Retiro): Llenamos con guiones de forma limpia
        if (realStudentInPartial == null)
        {
            foreach (var subId in orderedSubjectIdList)
            {
                worksheet.Cell(headerRow, bodyColumn).Value = "-";
                worksheet.Cell(headerRow, bodyColumn + 1).Value = "-";
                bodyColumn += 2;
            }

            worksheet.Cell(headerRow, bodyColumn++).Value = "-";
            worksheet.Cell(headerRow, bodyColumn++).Value = "-";
            worksheet.Cell(headerRow, bodyColumn).Value = "-";
            return; // Finaliza la fila de este alumno de forma segura
        }

        // SI SÍ TIENE NOTAS: Corre el flujo normal que ya tenías
        var gradeList = realStudentInPartial.gradeList!.OrderBy(e => orderedSubjectIdList.IndexOf(e.subjectId))
            .ToList();

        foreach (var result in gradeList)
        {
            worksheet.Cell(headerRow, bodyColumn).Value = result.label;
            worksheet.Cell(headerRow, bodyColumn + 1).Value = result.grade;

            if (result.grade < 60)
            {
                worksheet.Cell(headerRow, bodyColumn + 1).Style.Fill.BackgroundColor = redColor;
            }

            bodyColumn += 2;
        }

        var average = realStudentInPartial.getAverage(partialId);
        worksheet.Cell(headerRow, bodyColumn++).Value = average?.getConductLabel() ?? "-";
        worksheet.Cell(headerRow, bodyColumn++).Value = average?.conductGrade.ToString("F2") ?? "-";
        worksheet.Cell(headerRow, bodyColumn).Value = average?.grade.ToString("F2") ?? "-";
    }

    private void setHeaderMulti(int headerRow, List<model.secretary.SubjectEntity> subjects)
    {
        var headerColumn = 2;
        setHeaderEnrollment(headerRow - 1);

        var headerStyle = worksheet.Range($"B{headerRow}:{lastColumnName}{headerRow}");
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        worksheet.Cell(headerRow, headerColumn++).Value = "N°";
        worksheet.Cell(headerRow, headerColumn++).Value = "Código";
        worksheet.Cell(headerRow, headerColumn++).Value = "CUP";
        worksheet.Cell(headerRow, headerColumn++).Value = "Nombre completo";
        worksheet.Cell(headerRow, headerColumn++).Value = "Sexo";

        foreach (var item in subjects)
        {
            var rangeString = $"{getColumnName(headerColumn)}{headerRow}:{getColumnName(headerColumn + 1)}{headerRow}";
            worksheet.Range(rangeString).Merge().Value = item.initials;
            headerColumn += 2;
        }

        var resRange = $"{getColumnName(headerColumn)}{headerRow}:{getColumnName(headerColumn + 1)}{headerRow}";
        worksheet.Range(resRange).Merge().Value = "DP";

        headerColumn += 2;
        worksheet.Cell(headerRow, headerColumn).Value = "Promedio";
    }

    private void setTitleMulti(string descriptionLabel)
    {
        const int titleRow = 2;
        var title = worksheet.Range($"B{titleRow}:{lastColumnName}{titleRow}").Merge();
        title.Value = "Colegio Bautista Libertad";
        title.Style.Font.Bold = true;
        title.Style.Font.FontSize = 14;
        title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow).Height = 25;

        var subTitle = worksheet.Range($"B{titleRow + 1}:{lastColumnName}{titleRow + 1}").Merge();
        subTitle.Value = $"Sabana {descriptionLabel} {schoolyear}";
        subTitle.Style.Font.Bold = true;
        subTitle.Style.Font.FontSize = 13;
        subTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        subTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow + 1).Height = 18;

        var subTitleTeacher = worksheet.Range($"B{titleRow + 2}:{lastColumnName}{titleRow + 2}").Merge();
        subTitleTeacher.Value = $"Docente guía: {teacher.fullName()}";
        subTitleTeacher.Style.Font.FontSize = 13;
        subTitleTeacher.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        subTitleTeacher.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    }

    private void setWithMulti(int headerRow, int lastRow)
    {
        var range = worksheet.Range($"{getColumnName(7)}{headerRow}:{lastColumnName}{lastRow}");
        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        foreach (var column in worksheet.Columns($"{getColumnName(6)}:{getColumnName(columnQuantity - 1)}"))
        {
            column.Width = 5.6;
        }
    }

    private void setColumnQuantityMulti(int quantity)
    {
        columnQuantity = 6 + 2 * quantity + 2 + 1;
    }

    private void setHeaderEnrollment(int headerRow)
    {
        var enrollmentTitle = worksheet.Range($"B{headerRow}:{lastColumnName}{headerRow}").Merge();
        enrollmentTitle.Value = enrollmentLabel;
        enrollmentTitle.Style.Font.Bold = true;
        enrollmentTitle.Style.Font.FontSize = 13;
        enrollmentTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        enrollmentTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(headerRow).Height = 18;
    }

    private string getQualitativeConductLabel(double grade)
    {
        if (grade >= 90) return "E";
        if (grade >= 80) return "MB";
        if (grade >= 70) return "B";
        return "R";
    }

    // Cumplimiento obligatorio del contrato abstracto de la clase base SheetBuilder
    protected override void setColumnQuantity()
    {
        // Intencionalmente vacío. Las columnas se calculan de manera dinámica por pestaña
        // usando el método setColumnQuantityMulti(int quantity).
    }
}