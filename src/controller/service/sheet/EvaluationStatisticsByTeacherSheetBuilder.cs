using ClosedXML.Excel;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service.sheet;

public class EvaluationStatisticsByTeacherSheetBuilder : SheetBuilder
{
    private int partial { get; set; }
    private string schoolyear { get; set; } = null!;
    private string enrollmentLabel { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;

    private List<StudentEntity> studentList { get; set; } = null!;
    private List<StudentEntity> initialStudentList { get; set; } = null!;
    private List<SubjectPartialEntity> subjectPartialList { get; set; } = null!;
    private List<model.secretary.SubjectEntity> subjectList { get; set; } = null!;

    protected override void setColumnQuantity()
    {
        columnQuantity = 17;
    }

    public override byte[] getSpreadSheet()
    {
        using var workbook = new XLWorkbook();
        initWorksheet(workbook, enrollmentLabel);

        setTitle();
        setDate(6, userAlias);
        const int headerRow = 10;
        
        setHeaderEnrollment(headerRow - 2);
        setHeaderSummary(headerRow);
        setSummary(headerRow + 1);
        setDistribution(headerRow + 10);
        
        setHeaderSubject(headerRow);
        setSubject(headerRow + 2);

        worksheet.Columns().AdjustToContents();
        
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
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
        subTitle.Value = $"Datos estadísticos {PartialEntity.getLabel(partial)} {schoolyear}";
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
    
    private void setHeaderSummary(int headerRow)
    {
        var headerColumn = 2;

        var headerStyle = worksheet.Range($"B{headerRow}:{getColumnName(5)}{headerRow}");
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        worksheet.Cell(headerRow, headerColumn++).Value = "Evaluación";
        worksheet.Cell(headerRow, headerColumn++).Value = "Total";
        worksheet.Cell(headerRow, headerColumn++).Value = "Varones";
        worksheet.Cell(headerRow, headerColumn).Value = "Mujeres";
    }

    private void setSummary(int headerRow)
    {
        const int bodyColumn = 2;

        var initialTotal = initialStudentList.Count;
        var initialMaleCount = initialStudentList.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn, "Matrícula inicial", initialMaleCount, initialTotal);
        
        var total = studentList.Count;
        var maleCount = studentList.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn, "Matrícula actual", maleCount, total);
        
        var malePercentage = (decimal) maleCount / initialMaleCount;
        var femalePercentage = (decimal) (total - maleCount) / (initialTotal - initialMaleCount);
        var totalPercentage = (decimal) total / initialTotal;
        addRowPercentage(headerRow++, bodyColumn, "Retención de matrícula (%)", malePercentage, femalePercentage, totalPercentage);
        
        var approvedList = studentList.Where(e => e.passedAllSubjects()).ToList();
        var approvedTotal = approvedList.Count;
        var approvedMaleCount = approvedList.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn, "Aprobados", approvedMaleCount, approvedTotal);

        malePercentage = (decimal) approvedMaleCount / maleCount;
        femalePercentage = (decimal) (approvedTotal - approvedMaleCount) / (total - maleCount);
        totalPercentage = (decimal) approvedTotal / total;
        addRowPercentage(headerRow++, bodyColumn, "Rendimiento académico (%)", malePercentage, femalePercentage, totalPercentage);
        
        var failedFromOneToTwoList = studentList.Where(e => e.isFailed(1)).ToList();
        total = failedFromOneToTwoList.Count;
        maleCount = failedFromOneToTwoList.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn,"Aplazados de 1 a 2", maleCount, total);
        
        var failedFromThreeToMoreList = studentList.Where(e => e.isFailed(2)).ToList();
        total = failedFromThreeToMoreList.Count;
        maleCount = failedFromThreeToMoreList.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn,"Aplazados de 3 a más", maleCount, total);
        
        var notEvaluatedList = studentList.Where(e => e.hasNotEvaluated()).ToList();
        total = notEvaluatedList.Count;
        maleCount = notEvaluatedList.Count(e => e.student.sex);
        addRow(headerRow, bodyColumn,"No evaluados", maleCount, total);
    }

    private void setHeaderSubject(int headerRow)
    {
        var headerColumn = 8;
        
        var headerStyle = worksheet.Range($"{getColumnName(headerColumn)}{headerRow}:{getColumnName(17)}{headerRow+1}");
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        headerStyle.Style.Border.TopBorder = XLBorderStyleValues.Thin;
        headerStyle.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        headerStyle.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        headerStyle.Style.Border.RightBorder = XLBorderStyleValues.Thin;
        headerStyle.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        worksheet.Cell(headerRow, headerColumn).Value = "Asignatura";
        
        var cells = worksheet.Range($"{getColumnName(headerColumn + 1)}{headerRow}:{getColumnName(headerColumn + 3)}{headerRow}").Merge();
        cells.Value = "Aprobados";
        cells.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        cells.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        
        cells = worksheet.Range($"{getColumnName(headerColumn + 4)}{headerRow}:{getColumnName(headerColumn + 6)}{headerRow}").Merge();
        cells.Value = "Aplazados";
        cells.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        cells.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        
        cells = worksheet.Range($"{getColumnName(headerColumn + 7)}{headerRow}:{getColumnName(headerColumn + 9)}{headerRow++}").Merge();
        cells.Value = "No evaluados";
        cells.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        cells.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        
        worksheet.Cell(headerRow, headerColumn + 1).Value = "Total";
        worksheet.Cell(headerRow, headerColumn + 2).Value = "Varones";
        worksheet.Cell(headerRow, headerColumn + 3).Value = "Mujeres";
        worksheet.Cell(headerRow, headerColumn + 4).Value = "Total";
        worksheet.Cell(headerRow, headerColumn + 5).Value = "Varones";
        worksheet.Cell(headerRow, headerColumn + 6).Value = "Mujeres";
        worksheet.Cell(headerRow, headerColumn + 7).Value = "Total";
        worksheet.Cell(headerRow, headerColumn + 8).Value = "Varones";
        worksheet.Cell(headerRow, headerColumn + 9).Value = "Mujeres";
    }

    private void setSubject(int headerRow)
    {
        foreach (var item in subjectList)
        {
            setBody(item, headerRow++);
        }
    }

    private void setBody(model.secretary.SubjectEntity item, int headerRow)
    {
        var bodyColumn = 8;
        var result = subjectPartialList.FirstOrDefault(e => e.subjectId == item.subjectId);
        if (result == null)
        {
            return;
        }
        
        var approvedList = result.gradeList.Where(e => e.isApproved()).ToList();
        var failedList = result.gradeList.Where(e => !e.isApproved()).ToList();
        var notEvaluatedList = result.gradeList.Where(e => e.isNotEvaluated()).ToList();
        
        worksheet.Cell(headerRow, bodyColumn++).Value = item.initials;
        worksheet.Cell(headerRow, bodyColumn++).Value = approvedList.Count;
        worksheet.Cell(headerRow, bodyColumn++).Value = approvedList.Count(e => e.student!.sex);
        worksheet.Cell(headerRow, bodyColumn++).Value = approvedList.Count(e => !e.student!.sex);
        
        worksheet.Cell(headerRow, bodyColumn++).Value = failedList.Count;
        worksheet.Cell(headerRow, bodyColumn++).Value = failedList.Count(e => e.student!.sex);
        worksheet.Cell(headerRow, bodyColumn++).Value = failedList.Count(e => !e.student!.sex);
        
        worksheet.Cell(headerRow, bodyColumn++).Value = notEvaluatedList.Count;
        worksheet.Cell(headerRow, bodyColumn++).Value = notEvaluatedList.Count(e => e.student!.sex);
        worksheet.Cell(headerRow, bodyColumn).Value = notEvaluatedList.Count(e => !e.student!.sex);
    }
    
    private void addRow(int headerRow, int bodyColumn, string title, int male, int total)
    {
        worksheet.Cell(headerRow, bodyColumn).Value = title; 
        worksheet.Cell(headerRow, bodyColumn + 1).Value = total;
        worksheet.Cell(headerRow, bodyColumn + 2).Value = male;
        worksheet.Cell(headerRow, bodyColumn + 3).Value = total - male;
    }

    private void addRowPercentage(int headerRow, int bodyColumn, string title, decimal male, decimal female, decimal total)
    {
        worksheet.Cell(headerRow, bodyColumn).Value = title;
        worksheet.Cell(headerRow, bodyColumn + 1).Value = Math.Round(100 * total, 2);
        worksheet.Cell(headerRow, bodyColumn + 2).Value = Math.Round(100 * male, 2);
        worksheet.Cell(headerRow, bodyColumn + 3).Value = Math.Round(100 * female, 2);
    }

    private void setDistribution(int headerRow)
    {
        var bodyColumn = 2;
        
        var headerStyle = worksheet.Range($"B{headerRow}:E{headerRow++}").Merge();
        headerStyle.Value = "Evaluación cualitativa";
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        var list = studentList.Where(e => e.isWithInRange("AA", partial)).ToList();
        var total = list.Count;
        var maleCount = list.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn, "AA", maleCount, total);
        
        list = studentList.Where(e => e.isWithInRange("AS", partial)).ToList();
        total = list.Count;
        maleCount = list.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn, "AS", maleCount, total);
        
        list = studentList.Where(e => e.isWithInRange("AF", partial)).ToList();
        total = list.Count;
        maleCount = list.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn, "AF", maleCount, total);
        
        list = studentList.Where(e => e.isWithInRange("AI", partial)).ToList();
        total = list.Count;
        maleCount = list.Count(e => e.student.sex);
        addRow(headerRow++, bodyColumn, "AI", maleCount, total);
        
        total = studentList.Count;
        maleCount = studentList.Count(e => e.student.sex);
        addRow(headerRow, bodyColumn, "Totales", maleCount, total);
    }

    public class Builder
    {
        private readonly EvaluationStatisticsByTeacherSheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new EvaluationStatisticsByTeacherSheetBuilder();
        }

        public EvaluationStatisticsByTeacherSheetBuilder build() => sheetBuilder;

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

        public Builder withSubjectPartialList(List<SubjectPartialEntity> parameter)
        {
            sheetBuilder.subjectPartialList = parameter;
            return this;
        }
        
        public Builder withStudentList(List<StudentEntity> parameter)
        {
            sheetBuilder.studentList = parameter;
            return this;
        }
        
        public Builder withInitialStudentList(List<StudentEntity> parameter)
        {
            sheetBuilder.initialStudentList = parameter;
            return this;
        }
    }
}