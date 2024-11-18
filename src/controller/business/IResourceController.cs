using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IResourceController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task<string> getMedia(int type, string schoolyear);
}