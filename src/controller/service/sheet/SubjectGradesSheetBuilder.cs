using ClosedXML.Excel;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.config;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.sheet;

public class SubjectGradesSheetBuilder
{
    private UserEntity user { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;
    private EnrollmentEntity enrollment { get; set; } = null!;
    private List<SubjectPartialEntity> subjectPartialList { get; set; } = null!;
    
    private IXLWorksheet? worksheet { get; set; }
    
    public byte[] getSpreadSheet()
    {
        setLastColumnName();
        
        var title = $"Calificaciones {enrollment.label}";
        
        using var workbook = new XLWorkbook();
        worksheet = workbook.Worksheets.Add(title);
        worksheet.Style.Font.FontSize = 12;
        
        worksheet.CellsUsed().Style.NumberFormat.SetFormat("@");
        
        setTitle($"{title}. Docente: {teacher.fullName()}");
        setDate(5, user.getAlias());
        const int headerRow = 8;
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
        
        worksheet.Column(columnQuantity + 1).Hide();
        
        var lastRow = enrollment.studentList!.Count + headerRow;
        
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

    private void setBody(int headerRow, StudentEntity item, int pos)
    {
        var bodyColumn = 2;
        worksheet!.Cell(headerRow, bodyColumn++).Value = pos;
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
            worksheet!.Cell(headerRow, headerColumn++).Value = item.studentGrade!.grade;
        }

        var first = subjectPartialList.FirstOrDefault();
        if (first == null)
        {
            return;
        }
        
        first.setStudentGrade(studentId);
        worksheet!.Cell(headerRow, headerColumn++).Value = first.studentGrade!.conductGrade;
        worksheet!.Cell(headerRow, headerColumn).Value = studentId;
    }

    private void setHeader(int headerRow)
    {
        var headerColumn = 2;
        setHeaderId(headerRow - 1, headerColumn + 4);
        
        var headerStyle = worksheet!.Range($"B{headerRow}:{lastColumnName}{headerRow}");
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

    private void setHeaderId(int headerRow, int headerColumn)
    {
        foreach (var item in subjectPartialList)
        {
            worksheet!.Cell(headerRow, headerColumn++).Value = item.subjectId;
        }

        worksheet!.Row(headerRow).Hide();
    }

    private void setHeaderForSubject(int headerRow, int headerColumn)
    {
        foreach (var item in subjectPartialList)
        {
            var result = enrollment.subjectList!.FirstOrDefault(e => e.subjectId == item.subjectId);
            if (result == null)
            {
                continue;
            }
            
            worksheet!.Cell(headerRow, headerColumn++).Value = result.secretarySubject!.initials;
        }
        
        worksheet!.Cell(headerRow, headerColumn).Value = "Conducta";
    }
    
    private int columnQuantity { get; set; }
    
    private string lastColumnName { get; set; } = null!;
    
    private void setColumnQuantity()
    {
        var quantity = subjectPartialList.Count;
        columnQuantity = quantity + 6;
    }
    
    private void setLastColumnName()
    {
        lastColumnName = "";

        var counter = columnQuantity;
        while (counter > 0)
        {
            var modulo = (counter - 1) % 26;
            lastColumnName = Convert.ToChar(65 + modulo) + lastColumnName;
            counter = (counter - 1) / 26;
        }
    }
    
    public class Builder
    {
        private readonly SubjectGradesSheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new SubjectGradesSheetBuilder();
        }

        public SubjectGradesSheetBuilder build() => sheetBuilder;
        
        public Builder withUser(UserEntity parameter)
        {
            sheetBuilder.user = parameter;
            return this;
        }
        
        public Builder withTeacher(TeacherEntity parameter)
        {
            sheetBuilder.teacher = parameter;
            return this;
        }
        
        public Builder withEnrollment(EnrollmentEntity parameter)
        {
            sheetBuilder.enrollment = parameter;
            return this;
        }
        
        public Builder withSubjectPartialList(List<SubjectPartialEntity>? parameter)
        {
            if (parameter == null)
            {
                throw new InternalException("There is not subject grades.");
            }
            
            sheetBuilder.subjectPartialList = parameter.Distinct().ToList();
            sheetBuilder.setColumnQuantity();
            return this;
        }
    }
}