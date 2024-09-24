using wsmcbl.src.dto.secretary;

namespace wsmcbl.tests.integration;

public class EnrollStudentActionsTest : BaseActionsTest<EnrollStudentFixture>
{
    private readonly StudentFullDto student;
    public EnrollStudentActionsTest(EnrollStudentFixture factory) : base(factory)
    {
        baseUri = "/v1/secretary/enrollments";
        student = factory.getStudent().mapToDto();
    }

    [Fact]
    public async Task getStudentList_ShouldReturnJsonWithList_WhenCalled()
    {
        await assertNotEmptyList<BasicStudentToEnrollDto>($"{baseUri}/students");
    }
    
    [Fact]
    public async Task getStudentById_ShouldReturnJsonWithStudent_WhenCalled()
    {
        var response = await client.GetAsync($"{baseUri}/students/{student.studentId}");

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        var entity = deserialize<StudentFullDto>(content);
        Assert.NotNull(entity);
    }
    
    [Fact]
    public async Task saveEnroll_ShouldThrowException_WhenBadRequest()
    {
        var stringContent = getContentByJson("BadEnrollStudentDto.json");
        var response = await client.PutAsync(baseUri, stringContent);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }
    
    [Fact]
    public async Task saveEnroll_ShouldEnrollStudent_WhenCalled()
    {
        var enrollStudentDto = new EnrollStudentDto
        {
            enrollmentId = "en-1",
            student = student
        };
        var stringContent = getContentByDto(enrollStudentDto);

        var response = await client.PutAsync(baseUri, stringContent);
        
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task getDegreeList_ShouldReturnJsonWithList_WhenCalled()
    {
        await assertNotEmptyList<BasicDegreeToEnrollDto>($"{baseUri}/degrees");
    }

    private async Task assertNotEmptyList<TDto>(string uri)
    {
        var response = await client.GetAsync(uri);

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        var list = deserialize<List<TDto>>(content);
        Assert.NotNull(list);
        Assert.NotEmpty(list);
    }
}