using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.service;

public class InvoiceLatexBuilder : LatexBuilder
{
    public InvoiceLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
    }

    protected override string getTemplateName()
    {
        throw new NotImplementedException();
    }

    protected override string updateContent(string content)
    {
        throw new NotImplementedException();
    }

    public void setTransaction(TransactionEntity transaction)
    {
        getTemplateName();
        throw new NotImplementedException();
    }
}