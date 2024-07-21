using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.model.secretary;

public class SubjectEntityTest
{
    [Fact]
    public void SubjectEntity_ShouldSetValues_WhenParameterSubjectDataEntityIsProvide()
    {
        var subjectData = TestEntityGenerator.aSubjectData();

        var sut = new SubjectEntity(subjectData);
        
        Assert.Equal(subjectData.name, sut.name);
        Assert.Equal(subjectData.isMandatory, sut.isMandatory);
        Assert.Equal(subjectData.semester, sut.semester);
    }
}