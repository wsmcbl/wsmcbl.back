using ClosedXML.Excel;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.controller.service.sheet;

public class EvaluationStatisticsByLevelSheetBuilder : SheetBuilder
{
    private PartialEntity partial { get; set; } = null!;
    private string schoolyear { get; set; } = null!;
    private DaoFactory daoFactory { get; set; } = null!;
    private List<DegreeEntity> degreeList { get; set; } = null!;
    private List<model.secretary.SubjectEntity> subjectList { get; set; } = null!;
    
    protected override void setColumnQuantity()
    {
        columnQuantity = 10;
    }

    public override byte[] getSpreadSheet() => [];

    public override async Task<byte[]> getSpreadSheetAsync()
    {
        using var workbook = new XLWorkbook();

        await setPrimaryLevel(workbook);
        setSecondaryLevel(workbook);
        
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
    }

    private async Task setPrimaryLevel(XLWorkbook workbook)
    {
        const string level = "Primaria";
        initWorksheet(workbook, level);
        setTitle(level);
        setTitleRow(2);
        await setSubjectListByLevel(1);

        foreach (var item in degreeList)
        {
            await setListByDegree(item.degreeId!);
            setBodyByDegree(item);
        }
    }

    private async Task setSubjectListByLevel(int level)
    {
        subjectList = await daoFactory.subjectDao!.getListForCurrentSchoolyearByLevel(level);
    }

    private List<model.academy.StudentEntity> studentList { get; set; } = null!;
    private List<StudentEntity> initialStudentList { get; set; } = null!;
    private List<SubjectPartialEntity> subjectPartialList { get; set; } = null!;
    
    private async Task setListByDegree(string degreeId)
    {
        studentList = await daoFactory
            .academyStudentDao!.getListWithGradesByDegreeId(degreeId, partial.partialId);
        
        initialStudentList = await getListBeforeFirstPartialByDegree(degreeId);
        subjectPartialList = await daoFactory.subjectPartialDao!.getListByPartialIdAndDegreeId(partial.partialId, degreeId);
    }

    private async Task<List<StudentEntity>> getListBeforeFirstPartialByDegree(string degreeId)
    {
        var result = await daoFactory.academyStudentDao!.getListBeforeFirstPartialByDegreeId(degreeId);
        var initialList = result.Select(e => e.student).ToList();
        
        var list = await daoFactory.withdrawnStudentDao!.getListByDegreeId(degreeId);
        var withdrawnStudentList = list.Select(e => e.student!).ToList();
        
        return initialList.Union(withdrawnStudentList).ToList();
    }

    private void setSecondaryLevel(XLWorkbook workbook)
    {
        const string level = "Secundaria";
        initWorksheet(workbook, level);
        setTitle(level);
        setTitleRow(5);
    }

    private void setTitle(string level)
    {
        lastColumnName = getColumnName(10);
        
        const int titleRow = 2;
        var title = worksheet.Range($"B{titleRow}:{lastColumnName}{titleRow}").Merge();
        title.Value = "Colegio Bautista Libertad";
        title.Style.Font.Bold = true;
        title.Style.Font.FontSize = 14;
        title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow).Height = 25;

        var subTitle = worksheet.Range($"B{titleRow + 1}:{lastColumnName}{titleRow + 1}").Merge();
        subTitle.Value = $"Datos estadísticos {partial.label}, {level} {schoolyear}";
        subTitle.Style.Font.Bold = true;
        subTitle.Style.Font.FontSize = 13;
        subTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        subTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow + 1).Height = 18;
    }

    private void setTitleRow(int column)
    {
        var row = 10;
        cell(row++, column, "Matrícual");
        worksheet.Cell(row++, column).Value = "Inicial";
        worksheet.Cell(row, column).Value = "Actual";

        row += 3;
        cell(row++, column,"Asignaturas");
        worksheet.Cell(row++, column).Value = "Aprobados limpios";
        worksheet.Cell(row++, column).Value = "Reprobados de 1 a 2";
        worksheet.Cell(row, column).Value = "Reprobados de 3 a más";

        row += 3;
        cell(row++, column,"Asignatura");
        
        foreach (var item in (List<string>)["Hola", "Qué ta"])
        {
            worksheet.Cell(row++, column).Value = item;
        }

        row += 2;
        cell(row++, column, "Promedio");
        worksheet.Cell(row++, column).Value = "AA (100 - 90)";
        worksheet.Cell(row++, column).Value = "AS (89 - 76)";
        worksheet.Cell(row++, column).Value = "AF (75 - 60)";
        worksheet.Cell(row, column).Value = "AI (59 - 0)";
    }

    private void cell(int row, int column, string value)
    {
        var headerStyle = worksheet.Cell(row, column);
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Value = value;
    }

    private void setBodyByDegree(DegreeEntity degree)
    {
        
    }

    public class Builder
    {
        private readonly EvaluationStatisticsByLevelSheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new EvaluationStatisticsByLevelSheetBuilder();
        }

        public EvaluationStatisticsByLevelSheetBuilder build() => sheetBuilder;

        public Builder withPartial(PartialEntity parameter)
        {
            sheetBuilder.partial = parameter;
            return this;
        }

        public Builder withSchoolyear(string parameter)
        {
            sheetBuilder.schoolyear = parameter;
            return this;
        }

        public Builder witDegreeList(List<DegreeEntity> parameter)
        {
            sheetBuilder.degreeList = parameter;
            return this;
        }

        public Builder withDaoFactory(DaoFactory parameter)
        {
            sheetBuilder.daoFactory = parameter;
            return this;
        }
    }
}