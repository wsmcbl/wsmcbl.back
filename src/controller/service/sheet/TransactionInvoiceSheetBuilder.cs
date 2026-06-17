using ClosedXML.Excel;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.service.sheet;

public class TransactionInvoiceSheetBuilder : SheetBuilder
{
    private readonly List<TransactionInvoiceView> _transactions;

    public TransactionInvoiceSheetBuilder(List<TransactionInvoiceView> transactions)
    {
        _transactions = transactions;
        setColumnQuantity();
    }

    protected override void setColumnQuantity()
    {
        // Cantidad de columnas (B, C, D, E, F, G, H) = Total 7 columnas
        columnQuantity = 7; 
    }

    public override byte[] getSpreadSheet()
    {
        using (var workbook = new XLWorkbook())
        {
            // Inicializar la hoja con el nombre "Transacciones"
            initWorksheet(workbook, "Transacciones");

            // 1. Agregar un Título al reporte
            worksheet.Cell("B2").Value = "Reporte de Transacciones Colegio Bautista Libertad";
            worksheet.Cell("B2").Style.Font.Bold = true;
            worksheet.Cell("B2").Style.Font.FontSize = 16;
            
            // 2. Configurar Cabeceras de la Tabla en la Fila 4
            int headerRow = 4;
            string[] headers = { "Fecha", "Estudiante", "N_Recibo", "Monto", "Concepto", "Status", "Cajero" };
            
            for (int i = 0; i < headers.Length; i++)
            {
                // i=0 -> B, i=1 -> C, ..., i=6 -> H
                var cell = worksheet.Cell($"{getColumnName(i + 2)}{headerRow}");
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = blueColor;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // 3. Llenar los datos
            int currentRow = 5;
            foreach (var item in _transactions)
            {
                worksheet.Cell($"B{currentRow}").Value = item.date;
                worksheet.Cell($"C{currentRow}").Value = item.student;
                worksheet.Cell($"D{currentRow}").Value = item.number;
                
                var totalCell = worksheet.Cell($"E{currentRow}");
                totalCell.Value = item.total;
                totalCell.Style.NumberFormat.SetFormat("C$#,##0.00");
                totalCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                
                worksheet.Cell($"F{currentRow}").Value = item.concept;
                worksheet.Cell($"G{currentRow}").Value = item.isValid;
                worksheet.Cell($"G{currentRow}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell($"H{currentRow}").Value = item.cashier;
                
                if (!item.isValid)
                {
                    worksheet.Range($"B{currentRow}:H{currentRow}").Style.Fill.BackgroundColor = redColor;
                }

                currentRow++;
            }

            // 4. Agregar bordes a la tabla utilizando los métodos de tu clase abstracta (Abarcará hasta la H)
            setBorder(currentRow - 1, headerRow);

            // 5. Agregar fila con pie de página / fecha de generación
            setDate(currentRow + 1, "");

            // 6. Ajustar automáticamente las columnas al contenido (B hasta H)
            worksheet.Columns($"B:{lastColumnName}").AdjustToContents();

            // 7. Guardar en memoria y retornar los bytes
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}