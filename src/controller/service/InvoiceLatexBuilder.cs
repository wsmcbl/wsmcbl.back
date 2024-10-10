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
    private string exchangeRate = null!;

    protected override string getTemplateName() => "invoice";
 
    protected override string updateContent(string content)
    {
        content = content.Replace("numeration.value", $"{series}{number:00000000}");
        content = content.Replace("customer.name.value", student.fullName());
        content = content.Replace("customer.id.value", student.studentId);
        content = content.Replace("detail.value", getDetail());
        content = content.Replace("total.value", $"C\\$ {total:F2}");
        content = content.Replace("discount.value", getDiscountTotal());
        content = content.Replace("arrears.value", getArrearsTotal());
        content = content.Replace("total.final.value", $"C\\$ {transaction.total:F2}");
        content = content.Replace("cashier.value", cashier.fullName());
        content = content.Replace("datetime.value", transaction.date.ToString(CultureInfo.InvariantCulture));
        content = content.Replace("exchange.rate.value", exchangeRate);
        content = content.Replace("general.balance.value", getGeneralBalance());

        return content;
    }

    private string getArrearsTotal() => $"C\\$ {arrearsTotal:F2}";
    private string getDiscountTotal() => $"C\\$ {discountTotal:F2}";

    private float discountTotal;
    private float arrearsTotal;
    private float total;
    private string getDetail()
    {
        var detail = transaction.details.Select(e => new
        {
            concept = e.concept(),
            amount = e.officialAmount(),
            arrears = e.calculateArrears(),
            discount = student.calculateDiscount(e.officialAmount()),
            isPaidLate = e.itPaidLate()
        });

        var content = "";
        foreach (var item in detail)
        {
            discountTotal += item.discount;
            arrearsTotal += item.arrears;
            total += item.amount;
            
            var concept = item.isPaidLate ? $"*{item.concept}" : item.concept;
            content = $"{content} {concept} & C\\$ {item.amount:F2}\\\\";
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

        public Builder withSeries(string parameter)
        {
            latexBuilder.series = parameter;
            return this;
        }

        public Builder withExchangeRate(float parameter)
        {
            latexBuilder.exchangeRate = $"{parameter:F2}";
            return this;
        }
    }
}