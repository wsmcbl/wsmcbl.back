using System.Globalization;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.service;

public class InvoiceLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student = null!;
    private TransactionEntity transaction = null!;
    private CashierEntity cashier = null!;
    private float[] generalBalance = null!;
    private int number;
    private string series = null!;

    protected override string getTemplateName() => "invoice";
    
    /*
       \newcommand{\thenumber}{numeration.value}
       \newcommand{\clientName}{client.name.value}
       \newcommand{\detail}{detail.value}
       \newcommand{\total}{total.value}
       \newcommand{\cashier}{cashier.value}
       \newcommand{\datetime}{datetime.value}
       \newcommand{\generalBalance}{general.balance.value}
     */
    protected override string updateContent(string content)
    {
        content = content.Replace("numeration.value", $"{number.ToString()}-{series}");
        content = content.Replace("client.name.value", student.fullName());
        content = content.Replace("detail.value", getDetail());
        content = content.Replace("total.value", $"C\\$ {transaction.total:F2}");
        content = content.Replace("cashier.value", cashier.fullName());
        content = content.Replace("datetime.value", transaction.date.ToString(CultureInfo.InvariantCulture));
        content = content.Replace("general.balance.value", getGeneralBalance());

        return content;
    }

    private string getDetail()
    {
        var detail = transaction
            .details
            .Select(e 
                => new {
                    Concept = e.concept(),
                    OfficialAmount = e.officialAmount(),
                    CalculateArrear = e.calculateArrear(),
                    Amout = student.calculateDiscount(e.officialAmount())});

        var content = "";
        var item = detail.First();

        for (int i = 0; i < 40; i++)
        {
            
            var amount = item.OfficialAmount - item.Amout + item.CalculateArrear;
            content = $"{content} {item.Concept} & C\\$ {amount}\\\\";
        }

        return content;
    }

    private string getGeneralBalance()
    {
        var value = generalBalance[0] - generalBalance[1];
        return $"C\\$ {value:F2}";
    }
    
    public class Builder
    {
        private readonly InvoiceLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new InvoiceLatexBuilder(templatesPath, outPath);
        }

        public InvoiceLatexBuilder build() => latexBuilder;

        public Builder withStudent(StudentEntity parameter)
        {
            latexBuilder.student = parameter;
            return this;
        }

        public Builder withTransaction(TransactionEntity parameter)
        {
            latexBuilder.transaction = parameter;
            return this;
        }

        public Builder withCashier(CashierEntity parameter)
        {
            latexBuilder.cashier = parameter;
            return this;
        }

        public Builder withGeneralBalance(float[] parameter)
        {
            latexBuilder.generalBalance = parameter;
            return this;
        }

        public Builder withNumber(int parameter)
        {
            latexBuilder.number = parameter;
            return this;
        }

        public Builder withSerie(string parameter)
        {
            latexBuilder.series = parameter;
            return this;
        }
    }
}