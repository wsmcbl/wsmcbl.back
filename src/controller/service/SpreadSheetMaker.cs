using ClosedXML.Excel;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service;

public class SpreadSheetMaker
{
    private DaoFactory daoFactory {get; set;}

    private const string lastColumnName = "V";
    private IXLWorksheet? worksheet { get; set; }
    
    public SpreadSheetMaker(DaoFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }
    
    public async Task<byte[]> getStudentRegisterInCurrentSchoolyear(string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var registerList = await daoFactory.studentDao!.getStudentRegisterInCurrentSchoolyear();

        var title = $"Padrón {currentSchoolyear.label}";
        
        using var workbook = new XLWorkbook();
        worksheet = workbook.Worksheets.Add(title);
        worksheet.Style.Font.FontSize = 12;
        
        worksheet.CellsUsed().Style.NumberFormat.SetFormat("@");
        
        setTitle(title);
        setDate(5, user.getAlias());
        const int headerRow = 7;
        setHeader(headerRow);
        
        var list = registerList
            .OrderBy(e => e.degreePosition)
            .ThenBy(e => e.sectionPosition)
            .ThenBy(e => e.fullName);
        
        var counter = headerRow + 1;
        foreach (var item in list)
        {
            setBody(counter, item, counter - headerRow);
            counter++;
        }

        var lastRow = registerList.Count + headerRow;
        
        setBorder(lastRow, headerRow);
        
        worksheet.Columns().AdjustToContents();
        worksheet.SheetView.FreezeRows(headerRow);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
    }

    private void setDate(int row, string alias)
    {
        var dateCell = worksheet!.Range($"B{row}:{lastColumnName}{row}").Merge();
        dateCell.Value = $"Generado por wsmcbl el {DateTime.UtcNow.toStringUtc6()}, {alias}.";
        dateCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
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

    private void setTitle(string title)
    { 
        var titleRow = 2;
        var titleCell = worksheet!.Range($"B{titleRow}:{lastColumnName}{titleRow}").Merge();
        titleCell.Value = "Colegio Bautista Libertad";
        titleCell.Style.Font.Bold = true;
        titleCell.Style.Font.FontSize = 14;
        titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        titleCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow).Height = 25;
        
        
        var titleCell1 = worksheet.Range($"B{titleRow + 1}:{lastColumnName}{titleRow + 1}").Merge();
        titleCell1.Value = title;
        titleCell1.Style.Font.Bold = true;
        titleCell1.Style.Font.FontSize = 13;
        titleCell1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        titleCell1.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(titleRow + 1).Height = 20;
    }

    private void setBody(int counter, StudentRegisterView item, int pos)
    {
        var bodyColumn = 2;
        worksheet!.Cell(counter, bodyColumn++).Value = pos;
        worksheet.Cell(counter, bodyColumn++).Value = item.studentId; 
        worksheet.Cell(counter, bodyColumn++).Value = item.minedId.getOrDefault();
        worksheet.Cell(counter, bodyColumn++).Value = item.educationalLevel; 
        worksheet.Cell(counter, bodyColumn++).Value = item.degree; 
        worksheet.Cell(counter, bodyColumn++).Value = item.section;  
        worksheet.Cell(counter, bodyColumn++).Value = item.fullName; 
        worksheet.Cell(counter, bodyColumn++).Value = item.birthday.ToString("dd-MM-yyyy");
        worksheet.Cell(counter, bodyColumn++).Value = item.getAge();  
        worksheet.Cell(counter, bodyColumn++).Value = item.sex ? "M" : "F"; 
        worksheet.Cell(counter, bodyColumn++).Value = item.weight; 
        worksheet.Cell(counter, bodyColumn++).Value = item.height;  
        worksheet.Cell(counter, bodyColumn++).Value = item.address;  
        worksheet.Cell(counter, bodyColumn++).Value = item.mother; 
        worksheet.Cell(counter, bodyColumn++).Value = item.motherIdCard; 
        worksheet.Cell(counter, bodyColumn++).Value = item.father; 
        worksheet.Cell(counter, bodyColumn++).Value = item.fatherIdCard;
        worksheet.Cell(counter, bodyColumn++).Value = item.tutor; 
        worksheet.Cell(counter, bodyColumn++).Value = item.phone; 
        worksheet.Cell(counter, bodyColumn++).Value = item.getIsRepeatingString();
        worksheet.Cell(counter, bodyColumn++).Value = item.getEnrollDateString();
    }

    private void setHeader(int headerRow)
    { 
        var headerStyle = worksheet!.Range($"B{headerRow}:{lastColumnName}{headerRow}");
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        
        var headerColumn = 2;
        worksheet.Cell(headerRow, headerColumn++).Value = "N°";
        worksheet.Cell(headerRow, headerColumn++).Value = "Código";
        worksheet.Cell(headerRow, headerColumn++).Value = "Cód. mined";
        worksheet.Cell(headerRow, headerColumn++).Value = "Modalidad";
        worksheet.Cell(headerRow, headerColumn++).Value = "Nivel";
        worksheet.Cell(headerRow, headerColumn++).Value = "Sección";
        worksheet.Cell(headerRow, headerColumn++).Value = "Nombre completo";
        worksheet.Cell(headerRow, headerColumn++).Value = "F. nacimiento";
        worksheet.Cell(headerRow, headerColumn++).Value = "Edad";
        worksheet.Cell(headerRow, headerColumn++).Value = "Sexo";
        worksheet.Cell(headerRow, headerColumn++).Value = "Peso";
        worksheet.Cell(headerRow, headerColumn++).Value = "Talla";
        worksheet.Cell(headerRow, headerColumn++).Value = "Dirección";
        worksheet.Cell(headerRow, headerColumn++).Value = "Madre";
        worksheet.Cell(headerRow, headerColumn++).Value = "Céd. madre";
        worksheet.Cell(headerRow, headerColumn++).Value = "Padre";
        worksheet.Cell(headerRow, headerColumn++).Value = "Céd. padre";
        worksheet.Cell(headerRow, headerColumn++).Value = "Tutor";
        worksheet.Cell(headerRow, headerColumn++).Value = "Teléfono";
        worksheet.Cell(headerRow, headerColumn++).Value = "Repitente";
        worksheet.Cell(headerRow, headerColumn++).Value = "F. matrícula";
    }
}