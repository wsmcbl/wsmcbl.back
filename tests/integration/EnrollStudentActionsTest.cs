using wsmcbl.src.dto.secretary;
using wsmcbl.tests.integration.util;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

[CollectionDefinition("SequentialCollection")]
[TestCaseOrderer("wsmcbl.tests.integration.util.PriorityOrderer", "wsmcbl.tests")]
public class EnrollStudentActionsTest : BaseActionsTest<EnrollStudentFixture>
{
    private EnrollStudentDto student { get; set; }

    public EnrollStudentActionsTest(EnrollStudentFixture factory) : base(factory)
    {
        baseUri = "/v3/secretary/enrollments";
        student = factory.getStudent().mapToDto(("enroll0001", 1, false));
    }

    [Fact]
    public async Task getStudentList_ShouldReturnJsonWithList_WhenCalled()
    {
        await assertNotEmptyList<BasicStudentToEnrollDto>($"{baseUri}/students");
    }
    
    [Fact]
    public async Task getStudentById_ShouldReturnJsonWithStudent_WhenCalled()
    {
        var response = await client.GetAsync($"{baseUri}/students/{student.getStudentId()}");

        await response.EnsureSuccess();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        var entity = deserialize<StudentFullDto>(content);
        Assert.NotNull(entity);
    }
    
    [Fact]
    public async Task getStudentById_ShouldReturnNotFound_WhenStudentNotExist()
    {
        var response = await client.GetAsync($"{baseUri}/students/nobody");
        
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task saveEnroll_ShouldReturnsNotFound_WhenBadRequest()
    {
        var stringContent = getContentByJson("BadEnrollStudentDto.json");
        var response = await client.PutAsync(baseUri, stringContent);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }
    
    [Fact, TestPriority(1)]
    public async Task saveEnroll_ShouldEnrollStudent_WhenCalled()
    {
        var enrollStudentDto = student;
        var stringContent = getContentByDto(enrollStudentDto);

        var response = await client.PutAsync(baseUri, stringContent);
        
        await response.EnsureSuccess();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task getDegreeList_ShouldReturnJsonWithList_WhenCalled()
    {
        await assertNotEmptyList<BasicDegreeToEnrollDto>($"{baseUri}/degrees");
    }

    [Fact, TestPriority(2)]
    public async Task getEnrollDocument_ShouldReturnByteArray_WhenCalled()
    {
        var response = await client.GetAsync($"{baseUri}/documents/{student.getStudentId()}");

        await response.EnsureSuccess();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsByteArrayAsync();
        Assert.NotNull(content);
        Assert.NotEmpty(content);
    }
    
    [Fact]
    public async Task getEnrollDocument_ShouldReturnNotFound_WhenStudentNotExist()
    {
        var response = await client.GetAsync($"{baseUri}/documents/{student.getStudentId()}");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    
    private async Task assertNotEmptyList<TDto>(string uri)
    {
        var response = await client.GetAsync(uri);

        await response.EnsureSuccess();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        var list = deserialize<List<TDto>>(content);
        Assert.NotNull(list);
        Assert.NotEmpty(list);
    }
}