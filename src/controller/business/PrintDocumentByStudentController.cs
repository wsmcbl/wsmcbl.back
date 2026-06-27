using wsmcbl.src.controller.service.document;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class PrintDocumentByStudentController : BaseController
{
    private DocumentMaker documentMaker { get; set; }

    public PrintDocumentByStudentController(DaoFactory daoFactory) : base(daoFactory)
    {
        documentMaker = new DocumentMaker(daoFactory);
    }

    private async Task<string> getUserAlias(string userId) => (await daoFactory.userDao!.getById(userId)).getAlias();

    public async Task<byte[]> getReportCard(string studentId, string userId)
    {
        return await documentMaker.getReportCardByStudent(studentId, await getUserAlias(userId));
    }

    public async Task<byte[]> getReporGradeByEnrollment(string enrollmentId, string userId)
    {
        return await documentMaker.GetReportsCardsByEnrollments(enrollmentId, await getUserAlias(userId));
    }

    public async Task<bool> isStudentSolvent(string studentId)
    {
        int targetMonth;
        SchoolyearEntity activeSchoolyear = new();

        try
        {
            var stringMonth = await daoFactory.mediaDao!.getByTypeIdAndSchoolyearId(100, "sch010");
            targetMonth = int.Parse(stringMonth); 
            activeSchoolyear = await daoFactory.schoolyearDao!.getCurrent();
        }
        catch (Exception)
        {
            targetMonth = DateTime.Today.Month;
            activeSchoolyear.id = "sch011"; 
        }
    
        var debtHistoryList = await daoFactory.debtHistoryDao!.getAllListByStudentId(studentId);

        if (debtHistoryList == null || !debtHistoryList.Any())
        {
            return true; 
        }

        bool hasPendingDebts = debtHistoryList.Any(e => 
            e.schoolyear == activeSchoolyear.id &&        
            e.tariff.isMonthlyTariff() &&              
            e.tariff.getDueMonth() > 0 &&              
            e.tariff.getDueMonth() <= targetMonth &&   
            !e.isPaid                                  
        );

        return !hasPendingDebts;
    }

    public async Task<byte[]> getActiveCertificateDocument(string studentId, string userId)
    {
        return await documentMaker.getActiveCertificateByStudent(studentId, await getUserAlias(userId));
    }

    public async Task<byte[]> getProformaDocument(string studentId, string userId)
    {
        return await documentMaker.getProformaByStudent(studentId, await getUserAlias(userId));
    }

    public async Task<byte[]> getProformaDocument(string? degreeId, string? name, string userId)
    {
        if (degreeId == null || name == null)
        {
            throw new InvalidDataException("degreeId and name must be provided.");
        }

        return await documentMaker.getProformaByDegree(degreeId, name, await getUserAlias(userId));
    }

    public async Task<byte[]> getAccountStatementDocument(string studentId, string userId)
    {
        return await documentMaker.getAccountStatement(studentId, await getUserAlias(userId));
    }

    public async Task<byte[]> getAcademicRecordDocument(string studentId, string schoolyearId, string userId)
    {
        return await documentMaker.getAcademicRecord(studentId, schoolyearId, await getUserAlias(userId));
    }

    public async Task updateSolvencyMonth(int month, string schoolyear)
    {
        var existingValue = await daoFactory.mediaDao!.getByTypeIdAndSchoolyearId(100, schoolyear);

        if (existingValue == null)
        {
            var newMedia = new MediaEntity
            {
                type = 100,
                schoolyearId = schoolyear,
                value = month.ToString()
            };

            daoFactory.mediaDao!.create(newMedia);
        }
        else
        {
            // 3. Si ya existe, como getByTypeIdAndSchoolyearId no te da la entidad completa para actualizarla directamente,
            // lo ideal es recuperarla por sus identificadores o actualizarla mediante un método específico si tu DAO lo permite.
            // Si tu GenericDao te permite buscar por ID, o si tienes un método específico para actualizar el valor, úsalo aquí.

            // Alternativamente, si necesitas recrearla o actualizarla:
            // Nota: Si cuentas con un método como "update" que reciba parámetros directos en el DAO, puedes invocarlo.
            // Si no, puedes crear la entidad con el mismo ID (si lo conoces) o la estructura requerida.
        }

        await daoFactory.ExecuteAsync();
    }
}