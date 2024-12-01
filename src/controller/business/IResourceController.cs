using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.controller.business;

public interface IResourceController
{
    public Task<List<(StudentEntity student, string schoolyear, string enrollment)>> getStudentList();
    public Task<string> getMedia(int type, int schoolyear);
    public Task<MediaEntity> updateMedia(MediaEntity media);
    public Task<MediaEntity> createMedia(MediaEntity media);
    public Task<List<MediaEntity>> getMediaList();
    public Task<DebtHistoryEntity> forgiveADebt(string studentId, int tariffId);
}