using wsmcbl.src.exception;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.model.secretary;

public class DegreeEntityTest
{
    [Fact]
    public void DegreeEntity_ShouldAddSubjects_WhenParameterIsProvide()
    {
        var degreeData = TestEntityGenerator.aDegreeData();

        var sut = new DegreeEntity(degreeData, "sch001");

        Assert.NotNull(sut.subjectList);
        Assert.NotEmpty(sut.subjectList);
        Assert.Equal(degreeData.subjectList!.Count, sut.subjectList.Count);
        Assert.Equal(degreeData.getModalityName(), sut.modality);
        Assert.Equal(degreeData.label, sut.label);
    }

    [Fact]
    public void CreateEnrollments_ShouldCreateEnrollments_WhenParameterIsCorrect()
    {
        var degreeData = TestEntityGenerator.aDegreeData();

        var sut = new DegreeEntity(degreeData, "sch001");

        sut.createEnrollments(3);

        Assert.NotNull(sut.enrollmentList);
        Assert.Equal(3, sut.enrollmentList.Count);
    }

    [Fact]
    public void CreateEnrollments_ShouldThrowException_WhenParameterIsIncorrect()
    {
        var degreeData = TestEntityGenerator.aDegreeData();

        var sut = new DegreeEntity(degreeData, "sch001");

        Assert.Throws<BadRequestException>(() => sut.createEnrollments(0));
    }

    [Fact]
    public void CreateEnrollments_ShouldThrowException_WhenEnrollmentListContainsElements()
    {
        var degreeData = TestEntityGenerator.aDegreeData();

        var sut = new DegreeEntity(degreeData, "sch001");
        sut.createEnrollments(1);

        Assert.Throws<IncorrectDataBadRequestException>(() => sut.createEnrollments(1));
    }
}