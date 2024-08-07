using wsmcbl.src.dto.output;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.output;

public class DtoMapperTest
{
    [Fact]
    public void getInvoiceDto_ReturnsDto()
    {
        var invoiceDto = new TestDtoOutputGenerator().aInvoiceDto();
        
        var transactionEntity = TestEntityGenerator.aTransaction("std-1", []);

        var result = transactionEntity.mapToDto(TestEntityGenerator.aStudent("std-1"), TestEntityGenerator.aCashier("csh-1"));
        
        Assert.NotNull(result);
        Assert.Equivalent(invoiceDto, result);
    }
    
    
    [Fact]
    public void getDetailDto_ReturnsDto()
    {
        var entityGenerator = new TestEntityGenerator();
        var detailDto = new TestDtoOutputGenerator().aDetailDto(TestEntityGenerator.aTariff(), TestEntityGenerator.aStudent("std-1"));
        
        var transactionTariff = entityGenerator.aTransactionTariffEntity();

        var result = transactionTariff.mapToDto(TestEntityGenerator.aStudent("std-1"));

        Assert.NotNull(result);
        Assert.Equivalent(detailDto, result);
    }


    [Fact]
    public void getTariffDto_ReturnsDto()
    {
        var debtHistory = TestEntityGenerator.aDebtHistory("std-1", false);
        var tariffDto = new TestDtoOutputGenerator().aPaymentDto(debtHistory);

        var result = debtHistory.mapToDto();

        Assert.NotNull(result);
        Assert.Equivalent(tariffDto, result);
    }


    [Fact]
    public void getStudentBasicDtoList_ReturnsListDto()
    {
        var studentBasicDtoList = TestDtoOutputGenerator.aStudentBasicDtoList();

        List<StudentEntity> studentList = [TestEntityGenerator.aStudent("std-1")];

        var result = studentList.mapListTo();

        Assert.NotEmpty(result);
        Assert.Equivalent(studentBasicDtoList, result);
    }
}