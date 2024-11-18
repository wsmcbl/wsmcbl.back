using wsmcbl.src.model.config;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IResourceController
{
    public Task<List<StudentEntity>> getStudentList();
    public Task<string> getMedia(int type, int schoolyear);
    public Task<MediaEntity> updateMedia(MediaEntity media);
    public Task<MediaEntity> createMedia(MediaEntity media);
}