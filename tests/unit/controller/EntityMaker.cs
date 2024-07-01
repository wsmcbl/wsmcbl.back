using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.unit.controller;

public static class EntityMaker
{
    public static StudentEntity getObjetStudent(string studentId)
    {
        var secretaryStudent = new src.model.secretary.StudentEntity
        {
            name = "name1",
            surname = "surname1",
            tutor = "tutor1",
            schoolYear = "2024",
            enrollmentLabel = "7mo"
        };

        var discount = new DiscountEntity
        {
            discountId = 1,
            amount = 1000,
            description = "Description",
            tag = "A"
        };
        
        return new StudentEntity
        {
            studentId = studentId,
            student = secretaryStudent,
            discount = discount
        };
    }
    
    public static List<StudentEntity> getObjectStudentList() => [getObjetStudent("id1"), getObjetStudent("id2")];
    
    public static List<TariffEntity> getObjectTariffList()
    {
        var tariff = new TariffEntity
        {
            tariffId = 2,
            amount = 1000,
            concept = "concept",
            dueDate = new DateOnly(),
            isLate = true,
            schoolYear = "2024",
            type = 1
        };

        var list = new List<TariffEntity> { tariff };

        tariff.tariffId = 1;
        
        list.Add(tariff);

        return list;
    }
}