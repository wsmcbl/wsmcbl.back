using ClosedXML.Excel;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service.sheet;

public class SubjectGradesSheetBuilder : SheetBuilder
{
    private string userAlias { get; set; } = null!;
    private string schoolyear { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;
    private EnrollmentEntity enrollment { get; set; } = null!;

    private PartialEntity partial { get; set; } = null!;
    private List<SubjectPartialEntity> subjectPartialList { get; set; } = null!;
    private List<model.secretary.SubjectEntity> subjectList { get; set; } = null!;
    
    public override byte[] getSpreadSheet()
    {
        using var workbook = new XLWorkbook();
        initWorksheet(workbook, $"Calificaciones {enrollment.label}");
        
        setTitle();
        setDate(6, userAlias);
        const int headerRow = 10;
        setHeader(headerRow);
        
        var list = enrollment.studentList!
            .OrderBy(e => e.student.sex)
            .ThenBy(e => e.student.fullName());
        
        var counter = headerRow + 1;
        foreach (var item in list)
        {
            setBody(counter, item, counter - headerRow);
            counter++;
        }
        
        hideAndProtectCells(headerRow - 1);
        
        var lastRow = enrollment.studentList!.Count + headerRow;
        
        setBorder(lastRow, headerRow);
        
        worksheet.Columns().AdjustToContents();
        adjustToContents(headerRow, 6);
        worksheet.SheetView.FreezeRows(headerRow);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
    }

    private void adjustToContents(int headerRow, int columnSubject)
    {
        foreach (var column in worksheet.ColumnsUsed())
        {
            if (column.ColumnNumber() < columnSubject) continue;
            var cell = column.Cell(headerRow);
            var textLength = cell.GetString().Length;
            column.Width = textLength + 2;
        }
    }

    private void hideAndProtectCells(int headerRow)
    {
        worksheet.Row(headerRow).Hide();
        worksheet.Column(columnQuantity + 1).Hide();
        
        worksheet.Cells().Style.Protection.SetLocked(false);
        worksheet.Row(headerRow).Style.Protection.SetLocked(true);
        worksheet.Column(columnQuantity + 1).Style.Protection.SetLocked(true);
        worksheet.Protect("wsm");
    }

    private void setTitle()
    { 
        var titleRow = 2;
        var title = worksheet.Range($"B{titleRow}:{lastColumnName}{titleRow}").Merge();
        title.Value = "Colegio Bautista Libertad";
        title.Style.Font.Bold = true;
        title.Style.Font.FontSize = 14;
        title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow).Height = 25;
        
        var subTitle = worksheet.Range($"B{titleRow + 1}:{lastColumnName}{titleRow + 1}").Merge();
        subTitle.Value = $"Calificaciones {partial.label} {schoolyear}";
        subTitle.Style.Font.Bold = true;
        subTitle.Style.Font.FontSize = 13;
        subTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        subTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow + 1).Height = 18;
        
        var subTitleTeacher = worksheet.Range($"B{titleRow + 2}:{lastColumnName}{titleRow + 2}").Merge();
        subTitleTeacher.Value = $"Docente: {teacher.fullName()}";
        subTitleTeacher.Style.Font.FontSize = 13;
        subTitleTeacher.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        subTitleTeacher.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow + 1).Height = 18;
    }

    private void setBody(int headerRow, StudentEntity item, int pos)
    {
        var bodyColumn = 2;
        worksheet.Cell(headerRow, bodyColumn++).Value = pos;
        worksheet.Cell(headerRow, bodyColumn++).Value = item.studentId;  
        worksheet.Cell(headerRow, bodyColumn++).Value = item.fullName(); 
        worksheet.Cell(headerRow, bodyColumn++).Value = item.student.sex ? "M" : "F";
        setBodyForSubject(headerRow, bodyColumn, item.studentId);
    }

    private void setBodyForSubject(int headerRow, int headerColumn, string studentId)
    {
        foreach (var item in subjectPartialList)
        {
            item.setStudentGrade(studentId);
            worksheet.Cell(headerRow, headerColumn++).Value = item.studentGrade!.grade;
        }

        var first = subjectPartialList.FirstOrDefault();
        if (first == null)
        {
            return;
        }
        
        first.setStudentGrade(studentId);
        worksheet.Cell(headerRow, headerColumn++).Value = first.studentGrade!.conductGrade;
        worksheet.Cell(headerRow, headerColumn).Value = studentId;
    }

    private void setHeader(int headerRow)
    {
        var headerColumn = 2;
        setHeaderId(headerRow - 1, headerColumn + 2);

        setHeaderEnrollment(headerRow - 2);
        
        var headerStyle = worksheet.Range($"B{headerRow}:{lastColumnName}{headerRow}");
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        
        worksheet.Cell(headerRow, headerColumn++).Value = "N°";
        worksheet.Cell(headerRow, headerColumn++).Value = "Código";
        worksheet.Cell(headerRow, headerColumn++).Value = "Nombre completo";
        worksheet.Cell(headerRow, headerColumn++).Value = "Sexo";
        setHeaderForSubject(headerRow, headerColumn);
    }

    private void setHeaderEnrollment(int headerRow)
    {
        var enrollmentTitle = worksheet.Range($"B{headerRow}:{lastColumnName}{headerRow}").Merge();
        enrollmentTitle.Value = enrollment.label;
        enrollmentTitle.Style.Font.Bold = true;
        enrollmentTitle.Style.Font.FontSize = 13;
        enrollmentTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        enrollmentTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(headerRow).Height = 18;
    }

    private void setHeaderId(int headerRow, int headerColumn)
    {
        worksheet.Cell(headerRow, headerColumn).Value = teacher.teacherId;

        headerColumn += 2;
        foreach (var item in subjectPartialList)
        {
            worksheet.Cell(headerRow, headerColumn++).Value = item.subjectId;
        }
    }

    private void setHeaderForSubject(int headerRow, int headerColumn)
    {
        foreach (var item in subjectPartialList)
        {
            var result = subjectList!.FirstOrDefault(e => e.subjectId == item.subjectId);
            if (result == null)
            {
                continue;
            }
            
            worksheet.Cell(headerRow, headerColumn++).Value = result.initials;
        }
        
        worksheet.Cell(headerRow, headerColumn).Value = "Conducta";
    }
    
    protected override void setColumnQuantity()
    {
        var quantity = subjectPartialList.Count;
        columnQuantity = quantity + 6;
    }
    
    public class Builder
    {
        private readonly SubjectGradesSheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new SubjectGradesSheetBuilder();
        }

        public SubjectGradesSheetBuilder build() => sheetBuilder;
        
        public Builder withUserAlias(string parameter)
        {
            sheetBuilder.userAlias = parameter;
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
        
        public Builder withSubjectPartialList(List<SubjectPartialEntity>? parameter)
        {
            if (parameter == null)
            {
                throw new InternalException("There is not subject grades.");
            }
            
            sheetBuilder.subjectPartialList = parameter.Distinct().ToList();
            return this;
        }
        
        public Builder withPartial(PartialEntity parameter)
        {
            sheetBuilder.partial = parameter;
            return this;
        }
        
        public Builder withEnrollment(EnrollmentEntity parameter)
        {
            sheetBuilder.enrollment = parameter;
            sheetBuilder.setSubjectList();
            
            return this;
        }
    }

    private void setSubjectList()
    {
        subjectList = enrollment.subjectList!.Select(e => e.secretarySubject!)
            .Where(e => e.semester == 3 || e.semester == partial.semester)
            .OrderBy(e => e.areaId)
            .ThenBy(e => e.number)
            .ToList();

        var list = subjectList.Select(e => e.subjectId).ToList();
        
        subjectPartialList = subjectPartialList.OrderBy(x => list.IndexOf(x.subjectId))
            .ToList();
        
        setColumnQuantity();
    }
}