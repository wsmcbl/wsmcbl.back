using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.academy;

public interface IEnrollmentDao : IGenericDao<EnrollmentEntity, string>;
public interface ITeacherDao : IGenericDao<TeacherEntity, string>;