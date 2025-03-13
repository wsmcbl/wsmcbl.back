using wsmcbl.src.dto;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.tests.utilities;

public static class TestDtoInputGenerator
{

    public static TransactionDto aTransactionDto()
    {
        return new TransactionDto
        {
            cashierId = "csh1",
            studentId = "std-1",
            dateTime = DateTime.Now,
            details = []
        };
    }
    
    public static TransactionDetailDto aDetailDto()
    {
        return new TransactionDetailDto
        {
            tariffId = 10,
            amount = 700,
            applyArrears = true
        };
    }
    
    public static SubjectToCreateDto aSubjectDto()
    {
        return new SubjectToCreateDto
        {
            name = "Lengua y Literatura",
            isMandatory = true,
            semester = 2
        };
    }
}