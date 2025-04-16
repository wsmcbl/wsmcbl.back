using ClosedXML.Excel;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service.sheet;

public class EvaluationStatisticsByLevelSheetBuilder : SheetBuilder
{
    private PartialEntity partial { get; set; } = null!;
    private string schoolyear { get; set; } = null!;
    
    protected override void setColumnQuantity()
    {
        columnQuantity = 10;
    }

    public override byte[] getSpreadSheet()
    {
        using var workbook = new XLWorkbook();

        setPrimaryLevel(workbook);
        setSecondaryLevel(workbook);
        
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
    }

    private void setSecondaryLevel(XLWorkbook workbook)
    {
        const string level = "Secundaria";
        var sheet = workbook.Worksheets.Add(level);
        sheet.Style.Font.FontSize = 12;
        
        sheet.CellsUsed().Style.NumberFormat.SetFormat("@");
        setTitle(sheet, level);
    }

    private void setPrimaryLevel(XLWorkbook workbook)
    {
        const string level = "Primaria";
        var sheet = workbook.Worksheets.Add(level);
        sheet.Style.Font.FontSize = 12;
        
        sheet.CellsUsed().Style.NumberFormat.SetFormat("@");
        setTitle(sheet, level);
    }

    private void setTitle(IXLWorksheet sheet, string level)
    {
        const int titleRow = 2;
        var title = sheet.Range($"B{titleRow}:{lastColumnName}{titleRow}").Merge();
        title.Value = "Colegio Bautista Libertad";
        title.Style.Font.Bold = true;
        title.Style.Font.FontSize = 14;
        title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        sheet.Row(titleRow).Height = 25;

        var subTitle = sheet.Range($"B{titleRow + 1}:{lastColumnName}{titleRow + 1}").Merge();
        subTitle.Value = $"Datos estadísticos {partial.label}, {level} {schoolyear}";
        subTitle.Style.Font.Bold = true;
        subTitle.Style.Font.FontSize = 13;
        subTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        subTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        sheet.Row(titleRow + 1).Height = 18;
    }
}