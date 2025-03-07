using ClosedXML.Excel;
using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service;

public class SpreadSheetMaker
{
    private DaoFactory daoFactory {get; set;}
    
    public SpreadSheetMaker(DaoFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }
    
    public async Task<byte[]> getStudentRegisterInCurrentSchoolyear(string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var list = await daoFactory.studentDao!.getStudentRegisterInCurrentSchoolyear();
        
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add($"Padrón {currentSchoolyear.label}");
        
        worksheet.CellsUsed().Style.NumberFormat.SetFormat("@");
        
        // Aplicar estilo a los encabezados
        var headerStyle = worksheet.Range("B1:T1");
        headerStyle.Style.Font.Bold = true;
        headerStyle.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerStyle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        worksheet.Cell(1, 2).Value = user.getAlias();
        
        var header = 2;
        worksheet.Cell(header, 2).Value = "Código";
        worksheet.Cell(header, 3).Value = "Cód. Mined";
        worksheet.Cell(header, 4).Value = "Nombre";
        worksheet.Cell(header, 5).Value = "Modalidad";
        worksheet.Cell(header, 6).Value = "Grado";
        worksheet.Cell(header, 7).Value = "Seción";
        worksheet.Cell(header, 8).Value = "F. Matrícula";
        worksheet.Cell(header, 9).Value = "Sexo";
        worksheet.Cell(header, 10).Value = "F. Nacimiento";
        worksheet.Cell(header, 11).Value = "Edad";
        worksheet.Cell(header, 12).Value = "Talla";
        worksheet.Cell(header, 13).Value = "Peso";
        worksheet.Cell(header, 14).Value = "Tutor";
        worksheet.Cell(header, 15).Value = "Telefono";
        worksheet.Cell(header, 16).Value = "Padre";
        worksheet.Cell(header, 17).Value = "C. padre";
        worksheet.Cell(header, 18).Value = "Madre";
        worksheet.Cell(header, 19).Value = "C. Madre";
        worksheet.Cell(header, 20).Value = "Repitente";

        var counter = 3;
        foreach (var item in list.OrderBy(e => e.degreePosition))
        {
            worksheet.Cell(counter, 2).Value = item.studentId; 
            worksheet.Cell(counter, 3).Value = item.minedId.getOrDefault(); 
            worksheet.Cell(counter, 4).Value = item.fullName; 
            worksheet.Cell(counter, 5).Value = item.educationalLevel; 
            worksheet.Cell(counter, 6).Value = item.degree; 
            worksheet.Cell(counter, 7).Value = item.section; 
            worksheet.Cell(counter, 8).Value = item.enrollDate; 
            worksheet.Cell(counter, 9).Value = item.sex;
            worksheet.Cell(counter, 10).Value = item.birthday.ToString("dd-MM-yyyy"); 
            worksheet.Cell(counter, 11).Value = item.getAge(); 
            worksheet.Cell(counter, 12).Value = item.height; 
            worksheet.Cell(counter, 13).Value = item.weight; 
            worksheet.Cell(counter, 14).Value = item.tutor; 
            worksheet.Cell(counter, 15).Value = item.phone; 
            worksheet.Cell(counter, 16).Value = item.father; 
            worksheet.Cell(counter, 17).Value = item.fatherIdCard; 
            worksheet.Cell(counter, 18).Value = item.mother; 
            worksheet.Cell(counter, 19).Value = item.motherIdCard; 
            worksheet.Cell(counter, 20).Value = item.isRepeating.ToString() ?? "d";

            var rowStyle = worksheet.Range($"B{counter}:T{counter}");
            rowStyle.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            counter++;
        }

        worksheet.Columns().AdjustToContents(); // Ajustar ancho de columnas
        worksheet.SheetView.FreezeRows(1); // Fijar encabezado

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream.ToArray();
    }
}