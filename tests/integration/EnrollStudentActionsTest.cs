using wsmcbl.src.dto.secretary;

namespace wsmcbl.tests.integration;

public class EnrollStudentActionsTest : BaseActionsTest<EnrollStudentFixture>
{
    public EnrollStudentActionsTest(EnrollStudentFixture factory) : base(factory)
    {
        baseUri = "/v1/secretary/enrollments";
    }

    [Fact]
    public async Task getStudentList_ShouldReturnJsonWithList_WhenCalled()
    {
        await assertListWithOut<BasicStudentToEnrollDto>($"{baseUri}/degrees");
    }
    
    [Fact]
    public async Task getStudentById_ShouldReturnJsonWithStudent_WhenCalled()
    {
        var studentId = "2024-0001-kjtc";
        var response = await client.GetAsync($"{baseUri}/students/{studentId}");

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
    public async Task getGradeList_ShouldReturnJsonWithList_WhenCalled()
    {
        await assertListWithOut<BasicDegreeToEnrollDto>($"{baseUri}/degrees");
    }

    private async Task assertListWithOut<TDto>(string uri)
    {
        var response = await client.GetAsync(uri);

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        var list = deserialize<List<TDto>>(content);
        Assert.NotNull(list);
        //Assert.True(list.Count > 0);
    }
}