using wsmcbl.src.dto.accounting;
using wsmcbl.src.model;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.output;

public class DtoMapperTest
{
    [Fact]
    public void getStudentBasicDtoList_ReturnsListDto()
    {
        var studentBasicDtoList = TestDtoOutputGenerator.aStudentBasicDtoList();

        List<StudentView> studentList = TestEntityGenerator.aStudentList();

        var result = studentList.mapToList();

        Assert.NotEmpty(result);
        Assert.Equivalent(studentBasicDtoList, result);
    }
}