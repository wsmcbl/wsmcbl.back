using ClosedXML.Excel;
using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.sheet;

public class EnrollmentGradeSummarySheetBuilder : SheetBuilder
{
    private string partialLabel { get; set; } = null!;
    private string schoolyear { get; set; } = null!;
    private string enrollmentLabel { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;

    private List<model.secretary.SubjectEntity> subjectList { get; set; } = null!;
    private List<StudentEntity> studentList { get; set; } = null!;
    
    public override byte[] getSpreadSheet()
    {
        using var workbook = new XLWorkbook();
        initWorksheet(workbook, enrollmentLabel);
        
        setTitle();
        setDate(6, userAlias);
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
        var range = worksheet.Range($"{getColumnName(7)}{headerRow}:{lastColumnName}{lastRow}");
        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        
        foreach (var column in worksheet.Columns($"{getColumnName(6)}:{getColumnName(columnQuantity - 1)}")) 
        {
            column.Width = 5.6;
        }
    }

    private void setBody(int headerRow, StudentEntity student, int pos)
    {
        var bodyColumn = 2;
        worksheet.Cell(headerRow, bodyColumn++).Value = pos;
        worksheet.Cell(headerRow, bodyColumn++).Value = student.studentId;
        worksheet.Cell(headerRow, bodyColumn++).Value = student.student.minedId.getOrDefault();  
        worksheet.Cell(headerRow, bodyColumn++).Value = student.fullName(); 
        worksheet.Cell(headerRow, bodyColumn++).Value = student.student.sex ? "M" : "F";

        var gradeList = subjectList
            .Select(item => student.gradeList!.First(e => e.subjectId == item.subjectId)).ToList();
        
        foreach (var result in gradeList)
        {
            worksheet.Cell(headerRow, bodyColumn).Value = result.label;
            worksheet.Cell(headerRow, bodyColumn + 1).Value = result.grade;
            bodyColumn += 2;
        }

        worksheet.Cell(headerRow, bodyColumn++).Value = student.getAverage(1).getConductLabel();
        worksheet.Cell(headerRow, bodyColumn++).Value = student.getAverage(1).conductGrade.ToString("F2");
        
        worksheet.Cell(headerRow, bodyColumn ).Value = student.getAverage(1).grade.ToString("F2");
    }

    private void setHeader(int headerRow)
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
            result = worksheet
                .Range($"{getColumnName(headerColumn)}{headerRow}:{getColumnName(headerColumn + 1)}{headerRow}")
                .Merge();
            result.Value = item.initials;
            
            headerColumn += 2;
        }
        
        result = worksheet
            .Range($"{getColumnName(headerColumn)}{headerRow}:{getColumnName(headerColumn + 1)}{headerRow}")
            .Merge();
        result.Value = "Conducta";
            
        headerColumn += 2;
        worksheet.Cell(headerRow, headerColumn).Value = "Promedio";
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
        subTitle.Value = $"Sabana {partialLabel} {schoolyear}";
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
    
    protected override void setColumnQuantity()
    {
        var quantity = subjectList.Count;
        columnQuantity = 6 + 2*quantity + 2 + 1;
    }

    public class Builder
    {
        private readonly EnrollmentGradeSummarySheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new EnrollmentGradeSummarySheetBuilder();
        }

        public EnrollmentGradeSummarySheetBuilder build() => sheetBuilder;
        
        public Builder withPartial(string parameter)
        {
            sheetBuilder.partialLabel = parameter;
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
        
        public Builder withEnrollment(string parameter)
        {
            sheetBuilder.enrollmentLabel = parameter;
            return this;
        }
        
        public Builder withSubjectList(List<SubjectEntity> parameter)
        {
            sheetBuilder.subjectList = parameter.Select(e => e.secretarySubject!).ToList();
            sheetBuilder.setColumnQuantity();
            return this;
        }
        
        public Builder withStudentList(List<StudentEntity> parameter)
        {
            sheetBuilder.studentList = parameter;
            return this;
        }
    }
}