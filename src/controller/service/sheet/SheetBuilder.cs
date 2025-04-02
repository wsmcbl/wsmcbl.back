using ClosedXML.Excel;

namespace wsmcbl.src.controller.service.sheet;

public class SheetBuilder
{
    protected IXLWorksheet worksheet { get; set; } = null!;
    
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
}