using wsmcbl.src.model.academy;

namespace wsmcbl.tests.unit.model.academy;

public class EnrollmentEntityTest
{
    [Fact]
    public void setSubjectList_ShouldNotSetSubjectList_WhenParametersIsNoValid()
    {
        var sut = new EnrollmentEntity();
        
        sut.setSubjectList(new List<src.model.secretary.SubjectEntity>());
        
        Assert.Empty(sut.subjectList!);
    }
}