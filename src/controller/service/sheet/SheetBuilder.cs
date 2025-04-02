using ClosedXML.Excel;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.sheet;

public abstract class SheetBuilder
{
    protected int columnQuantity { get; set; }
    protected IXLWorksheet worksheet { get; set; } = null!;
    protected string lastColumnName { get; set; } = null!;
    
    protected abstract string updateContent(string value);
    protected abstract void setColumnQuantity();
    
    protected static string getColumnName(int value)
    {
        var result = "";
        while (value > 0)
        {
            var modulo = (value - 1) % 26;
            result = Convert.ToChar(65 + modulo) + result;
            value = (value - 1) / 26;
        }

        return result;
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
    
    private void setDate(int row, string userAlias)
    {
        var dateCell = worksheet!.Range($"B{row}:{lastColumnName}{row}").Merge();
        dateCell.Value = $"Generado por wsmcbl el {DateTime.UtcNow.toStringUtc6()}, {userAlias}.";
        dateCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    }
}