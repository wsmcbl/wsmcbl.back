using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.model.secretary;

public class GradeEntityTest
{

    [Fact]
    public void GradeEntity_ShouldAddSubjects_WhenParameterIsProvide()
    {
        var gradeData = TestEntityGenerator.aGradeData();
        
        var sut = new GradeEntity(gradeData, "sch001");
        
        Assert.NotEmpty(sut.subjectList);
    }
    
    [Fact]
    public void setSubjectList_ShouldSetSubjectList_WhenParameterIsProvide()
    {
        var sut = new GradeEntity();
        List<SubjectEntity> subjectList = [TestEntityGenerator.aSubject()];
        
        sut.setSubjectList(subjectList);
        
        Assert.Equivalent(subjectList, sut.subjectList);
    }
}