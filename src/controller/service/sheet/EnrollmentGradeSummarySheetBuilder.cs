using ClosedXML.Excel;
using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.sheet;

public class EnrollmentGradeSummarySheetBuilder
{
    private int partial { get; set; }
    private string schoolyear { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;
    private EnrollmentEntity enrollment { get; set; } = null!;

    private List<SubjectEntity> subjectList { get; set; } = null!;
    private List<StudentEntity> studentList { get; set; } = null!;
    
    private IXLWorksheet? worksheet { get; set; }
    
    public byte[] getSpreadSheet()
    {
        lastColumnName = getColumnName(columnQuantity);
        
        using var workbook = new XLWorkbook();
        worksheet = workbook.Worksheets.Add(enrollment.label);
        worksheet.Style.Font.FontSize = 12;
        
        worksheet.CellsUsed().Style.NumberFormat.SetFormat("@");
        
        setTitle();
        setDate(6);
        const int headerRow = 9;
        setHeader(headerRow);
        
        var counter = headerRow + 1;
        foreach (var item in studentList)
        {
            setBody(counter, item, counter - headerRow);
            counter++;
        }

        var lastRow = studentList.Count + headerRow;
        
        setBorder(lastRow, headerRow);
        worksheet.Columns().AdjustToContents();

        setWith(headerRow, lastRow);
        
        worksheet.SheetView.FreezeRows(headerRow);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
    }

    private void setWith(int headerRow, int lastRow)
    {
        var range = worksheet!.Range($"{getColumnName(7)}{headerRow}:{lastColumnName}{lastRow}");
        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        
        foreach (var column in worksheet.Columns($"{getColumnName(6)}:{getColumnName(columnQuantity - 1)}")) 
        {
            column.Width = 5.6;
        }
    }

    private void setBorder(int lastRow, int headerRow)
    {
        var tableRange = worksheet!.Range($"B{headerRow}:{lastColumnName}{lastRow}");

        tableRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
        tableRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        tableRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        tableRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
        tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
    }

    private void setBody(int headerRow, StudentEntity student, int pos)
    {
        var bodyColumn = 2;
        worksheet!.Cell(headerRow, bodyColumn++).Value = pos;
        worksheet.Cell(headerRow, bodyColumn++).Value = student.studentId;
        worksheet.Cell(headerRow, bodyColumn++).Value = student.student.minedId.getOrDefault();  
        worksheet.Cell(headerRow, bodyColumn++).Value = student.fullName(); 
        worksheet.Cell(headerRow, bodyColumn++).Value = student.student.sex ? "M" : "F";

        var gradeList = subjectList
            .Select(item => student.gradeList!.First(e => e.subjectId == item.subjectId)).ToList();
        
        foreach (var result in gradeList)
        {
            worksheet!.Cell(headerRow, bodyColumn).Value = result.label;
            worksheet!.Cell(headerRow, bodyColumn + 1).Value = result.grade;
            bodyColumn += 2;
        }

        worksheet!.Cell(headerRow, bodyColumn++).Value = student.getAverage(1).getConductLabel();
        worksheet!.Cell(headerRow, bodyColumn++).Value = student.getAverage(1).conductGrade.ToString("F2");
        
        worksheet!.Cell(headerRow, bodyColumn ).Value = student.getAverage(1).grade.ToString("F2");
    }

    private void setHeader(int headerRow)
    {
        var headerColumn = 2;
        setHeaderEnrollment(headerRow - 1);
        
        var headerStyle = worksheet!.Range($"B{headerRow}:{lastColumnName}{headerRow}");
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        
        worksheet.Cell(headerRow, headerColumn++).Value = "N°";
        worksheet.Cell(headerRow, headerColumn++).Value = "Código";
        worksheet.Cell(headerRow, headerColumn++).Value = "mined";
        worksheet.Cell(headerRow, headerColumn++).Value = "Nombre completo";
        worksheet.Cell(headerRow, headerColumn++).Value = "Sexo";
        setHeaderForSubject(headerRow, headerColumn);
    }

    private void setHeaderForSubject(int headerRow, int headerColumn)
    {
        IXLRange? result;
        foreach (var item in subjectList)
        {
            result = worksheet!
                .Range($"{getColumnName(headerColumn)}{headerRow}:{getColumnName(headerColumn + 1)}{headerRow}")
                .Merge();
            result.Value = item.getInitials;
            
            headerColumn += 2;
        }
        
        result = worksheet!
            .Range($"{getColumnName(headerColumn)}{headerRow}:{getColumnName(headerColumn + 1)}{headerRow}")
            .Merge();
        result.Value = "Conducta";
            
        headerColumn += 2;
        worksheet!.Cell(headerRow, headerColumn).Value = "Promedio";
    }

    private void setHeaderEnrollment(int headerRow)
    {
        var enrollmentTitle = worksheet!.Range($"B{headerRow}:{lastColumnName}{headerRow}").Merge();
        enrollmentTitle.Value = enrollment.label;
        enrollmentTitle.Style.Font.Bold = true;
        enrollmentTitle.Style.Font.FontSize = 13;
        enrollmentTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        enrollmentTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(headerRow).Height = 18;
    }

    private void setTitle()
    {
        var titleRow = 2;
        var title = worksheet!.Range($"B{titleRow}:{lastColumnName}{titleRow}").Merge();
        title.Value = "Colegio Bautista Libertad";
        title.Style.Font.Bold = true;
        title.Style.Font.FontSize = 14;
        title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow).Height = 25;
        
        var subTitle = worksheet.Range($"B{titleRow + 1}:{lastColumnName}{titleRow + 1}").Merge();
        subTitle.Value = $"Sabana {getPartial()} {schoolyear}";
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
        worksheet.Row(titleRow + 1).Height = 18;
    }

    private void setDate(int row)
    {
        var dateCell = worksheet!.Range($"B{row}:{lastColumnName}{row}").Merge();
        dateCell.Value = $"Generado por wsmcbl el {DateTime.UtcNow.toStringUtc6()}, {userAlias}.";
        dateCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    }
    
    private int columnQuantity { get; set; }
    
    private string lastColumnName { get; set; } = null!;
    
    private void setColumnQuantity()
    {
        var quantity = subjectList.Count;
        columnQuantity = 6 + 2*quantity + 2 + 1;
    }
    
    private static string getColumnName(int counter)
    {
        var result = "";
        while (counter > 0)
        {
            var modulo = (counter - 1) % 26;
            result = Convert.ToChar(65 + modulo) + result;
            counter = (counter - 1) / 26;
        }

        return result;
    }

    public class Builder
    {
        private readonly EnrollmentGradeSummarySheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new EnrollmentGradeSummarySheetBuilder();
        }

        public EnrollmentGradeSummarySheetBuilder build() => sheetBuilder;
        
        public Builder withPartial(int parameter)
        {
            sheetBuilder.partial = parameter;
            return this;
        }
        
        public Builder withSchoolyear(string parameter)
        {
            sheetBuilder.schoolyear = parameter;
            return this;
        }
        
        public Builder withTeacher(TeacherEntity parameter)
        {
            sheetBuilder.teacher = parameter;
            return this;
        }
        
        public Builder withUserAlias(string parameter)
        {
            sheetBuilder.userAlias = parameter;
            return this;
        }
        
        public Builder withEnrollment(EnrollmentEntity parameter)
        {
            sheetBuilder.enrollment = parameter;
            sheetBuilder.setSubjectList();
            return this;
        }
        
        public Builder withStudentList(List<StudentEntity> parameter)
        {
            sheetBuilder.studentList = parameter;
            return this;
        }
    }

    private void setSubjectList()
    {
        subjectList = enrollment.subjectList!
            .Where(e => e.secretarySubject!.semester == 3 || e.secretarySubject!.semester == getSemester())
            .OrderBy(e => e.secretarySubject!.areaId)
            .ThenBy(e => e.secretarySubject!.number).ToList();
        
        setColumnQuantity();
    }

    private int getSemester() => partial <= 2 ? 1 : 2;

    private string getPartial()
    {
        return partial switch
        {
            1 => "I parcial",
            2 => "II parcial",
            3 => "III parcial",
            4 => "IV parcial",
            _ => "Parcial desconocido"
        };
    }
}