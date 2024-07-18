using wsmcbl.src.dto.output;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.output;

public class DtoMapperTest
{
    [Fact]
    public void getInvoiceDto_ReturnsDto()
    {
        var entityGenerator = new TestEntityGenerator();
        var invoiceDto = new TestDtoGenerator().aInvoiceDto();
        
        var transactionEntity = entityGenerator.aTransaction("std-1", []);

        var result = transactionEntity.mapToDto(entityGenerator.aStudent("std-1"), entityGenerator.aCashier("csh-1"));
        
        Assert.NotNull(result);
        Assert.Equivalent(invoiceDto, result);
    }
    
    
    [Fact]
    public void getDetailDto_ReturnsDto()
    {
        var entityGenerator = new TestEntityGenerator();
        var detailDto = new TestDtoGenerator().aDetailDto(entityGenerator.aTariff(), entityGenerator.aStudent("std-1"));
        
        var transactionTariff = entityGenerator.aTransactionTariffEntity();

        var result = transactionTariff.mapToDto(entityGenerator.aStudent("std-1"));

        Assert.NotNull(result);
        Assert.Equivalent(detailDto, result);
    }


    [Fact]
    public void getTariffDto_ReturnsDto()
    {
        var entityGenerator = new TestEntityGenerator();
        var debtHistory = entityGenerator.aDebtHistory("std-1");
        var tariffDto = new TestDtoGenerator().aTariffDto(debtHistory);

        var result = debtHistory.mapToDto();

        Assert.NotNull(result);
        Assert.Equivalent(tariffDto, result);
    }


    [Fact]
    public void getStudentBasicDtoList_ReturnsListDto()
    {
        var entityGenerator = new TestEntityGenerator();
        var studentBasicDtoList = new TestDtoGenerator().aStudentBasicDtoList();

        List<StudentEntity> studentList = [entityGenerator.aStudent("std-1")];

        var result = studentList.mapListTo();

        Assert.NotEmpty(result);
        Assert.Equivalent(studentBasicDtoList, result);
    }
}