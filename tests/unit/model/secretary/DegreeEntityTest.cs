using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.model.secretary;

public class DegreeEntityTest
{

    [Fact]
    public void GradeEntity_ShouldAddSubjects_WhenParameterIsProvide()
    {
        var gradeData = TestEntityGenerator.aGradeData();
        
        var sut = new DegreeEntity(gradeData, "sch001");
        
        Assert.NotEmpty(sut.subjectList);
    }
    
    [Fact]
    public void setSubjectList_ShouldSetSubjectList_WhenParameterIsProvide()
    {
        var sut = new DegreeEntity();
        List<SubjectEntity> subjectList = [TestEntityGenerator.aSubject()];
        
        sut.setSubjectList(subjectList);
        
        Assert.Equivalent(subjectList, sut.subjectList);
    }
}