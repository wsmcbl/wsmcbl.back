namespace wsmcbl.src.dto.academy;

public class PrintReportCardByStudentDto
{
    public int lateArrivals { get; set; } 
    public int justifications { get; set; }
    public int unjustifications { get; set; }

    public (int lateArrivals, int justifications, int unjustifications) getTuple()
    {
        return (lateArrivals, justifications, unjustifications);
    }
}