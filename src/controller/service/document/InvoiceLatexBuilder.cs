using wsmcbl.src.model.accounting;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.document;

public class InvoiceLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student = null!;
    private TransactionEntity transaction = null!;
    private CashierEntity cashier = null!;
    private decimal[] generalBalance = null!;
    private int number;
    private string series = null!;
    private string exchangeRate = null!;

    protected override string getTemplateName() => "invoice";

    protected override string updateContent(string content)
    {
        content = content.ReplaceInLatexFormat("numeration.value", $"{series}{number:00000000}");
        content = content.ReplaceInLatexFormat("customer.name.value", student.fullName());
        content = content.ReplaceInLatexFormat("customer.id.value", student.studentId);
        content = content.Replace("detail.value", getDetail());
        content = content.ReplaceInLatexFormat("total.value", $"C$ {total:F2}");
        content = content.ReplaceInLatexFormat("discount.value", getDiscountTotal());
        content = content.ReplaceInLatexFormat("arrears.value", getArrearsTotal());
        content = content.ReplaceInLatexFormat("total.aux.value", getAuxTotal());
        content = content.ReplaceInLatexFormat("total.final.value", $"C$ {transaction.total:F2}");
        content = content.ReplaceInLatexFormat("cashier.value", cashier.getAlias());
        content = content.ReplaceInLatexFormat("datetime.value", transaction.date.toStringUtc6());
        content = content.ReplaceInLatexFormat("exchange.rate.value", exchangeRate);
        content = content.ReplaceInLatexFormat("general.balance.value", getGeneralBalance());

        return content;
    }

    private string getArrearsTotal() => $"C$ {arrearsTotal:F2}";
    private string getDiscountTotal() => $"C$ {discountTotal:F2}";
    private string getAuxTotal() => $"C$ {total + arrearsTotal - discountTotal:F2}";

    private decimal discountTotal;
    private decimal arrearsTotal;
    private decimal total;

    private string getDetail()
    {
        var content = "";
        
        foreach (var item in transaction.details)
        {
            discountTotal += item.discount;
            arrearsTotal += item.arrears;
            total += item.officialAmount();

            var tariffConcept = item.concept().ReplaceLatexSpecialSymbols();

            content = item.paidLate() ? $@"{content} *{tariffConcept} & C\$ {item.officialAmount():F2}\\" :
                $@"{content} {tariffConcept} & C\$ {item.officialAmount():F2}\\";
        }

        return content;
    }

    private string getGeneralBalance()
    {
        var value = generalBalance[1] - generalBalance[0];
        return $"C$ {value:F2}";
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

        public Builder withGeneralBalance(decimal[] parameter)
        {
            latexBuilder.generalBalance = parameter;
            return this;
        }

        public Builder withNumber(int parameter)
        {
            latexBuilder.number = parameter;
            return this;
        }

        public Builder withSeries(string parameter)
        {
            latexBuilder.series = parameter;
            return this;
        }

        public Builder withExchangeRate(decimal parameter)
        {
            latexBuilder.exchangeRate = $"{parameter:F2}";
            return this;
        }
    }
}