using wsmcbl.src.dto.accounting;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.output;

public class DtoMapperTest
{
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

        List<StudentEntity> studentList = [TestEntityGenerator.aAccountingStudent("std-1")];

        var result = studentList.mapListTo();

        Assert.NotEmpty(result);
        Assert.Equivalent(studentBasicDtoList, result);
    }
}