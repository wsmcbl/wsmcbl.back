using System.Globalization;
using wsmcbl.src.utilities;

namespace wsmcbl.src.model.accounting;

public class TransactionInvoiceView
{
    public string transactionId { get; set; } = null!;
    public int number { get; set; }
    public string studentId { get; set; } = null!;
    public string student { get; set; } = null!;
    public double total { get; set; }
    public bool isValid { get; set; }
    public string concept { get; set; } = null!;
    public string cashier { get; set; } = null!;
    public string? date { get; set; }
    public DateTime dateTime { get; set; }

    public void ChangeToUtc6()
    {
        dateTime = dateTime.toUTC6();
        date = dateTime.ToString("dd/MM/yyyy", new CultureInfo("es-ES"));
    }
}