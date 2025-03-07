using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ResourceController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<string> getMedia(int type, int schoolyear)
    {
        var result = await daoFactory.schoolyearDao!.getByLabel(schoolyear);
        return await daoFactory.mediaDao!.getByTypeAndSchoolyear(type, result.id!);
    }

    public async Task<MediaEntity> updateMedia(MediaEntity media)
    {
        var result = await daoFactory.mediaDao!.getById(media.mediaId);
        if (result == null)
        {
            throw new EntityNotFoundException("Media", media.mediaId.ToString());
        }
        
        result.update(media);
        await daoFactory.execute();

        return result;
    }

    public async Task<MediaEntity> createMedia(MediaEntity media)
    {
        daoFactory.mediaDao!.create(media);
        await daoFactory.execute();
        return media;
    }

    public async Task<List<MediaEntity>> getMediaList()
    {
        return await daoFactory.mediaDao!.getAll();
    }

    public async Task<List<string>> getTutorList()
    {
        var list = await daoFactory.studentTutorDao!.getAll();

        return list.Where(e => e.isValidPhone())
            .Select(e => e.phone[..8]).Distinct().ToList();
    }

    public async Task deleteStudentById(string studentId)
    {
        var debtList = await daoFactory.debtHistoryDao!.getListByStudentId(studentId);
        if(debtList.Count == 0)
            throw new EntityNotFoundException("Student", studentId);
        
        await daoFactory.debtHistoryDao!.deleteRange(debtList);
        
        var accountingStudent = await daoFactory.accountingStudentDao!.getById(studentId);
        if (accountingStudent == null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }
        
        await daoFactory.accountingStudentDao!.deleteAsync(accountingStudent);
        
        var student = await daoFactory.studentDao!.getById(studentId);
        if(student == null)
            throw new EntityNotFoundException("Student", studentId);

        await daoFactory.studentDao!.deleteAsync(student);
        
        var result = await daoFactory.studentTutorDao!.hasOnlyOneStudent(student.tutorId);
        if (result)
            return;

        var tutor = await daoFactory.studentTutorDao!.getById(student.tutorId);
        await daoFactory.studentTutorDao!.deleteAsync(tutor!);
    }

    public async Task<List<TransactionInvoiceView>> getTransactionInvoiceViewList(DateTime from, DateTime to)
    {
        return await daoFactory.transactionDao!.getTransactionInvoiceViewList(from, to);
    }
}