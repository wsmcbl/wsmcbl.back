using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IListController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task<string> getMedia(int type, string schoolyear);
}