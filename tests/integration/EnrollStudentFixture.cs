using wsmcbl.src.database.context;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

public class EnrollStudentFixture : BaseFixture
{
    protected override async Task seedData(PostgresContext context)
    {
        var schoolyear = TestEntityGenerator.aSchoolYear();
        schoolyear.id = "id";
        context.Add(schoolyear);
        await context.SaveChangesAsync();

        var degree = TestEntityGenerator.aDegree("degree");
        degree.schoolYear = schoolyear.id!;

        var student = TestEntityGenerator.aStudent("std");
        student.studentId = null;
        student.schoolYear = schoolyear.id!;

        var tariff = TestEntityGenerator.aTariff();
        tariff.type = 4;
        tariff.schoolYear = schoolyear.id!;

        var enrollment = TestEntityGenerator.aEnrollment();
        enrollment.schoolYear = schoolyear.id!;
        enrollment.degreeId = degree.degreeId!;

        context.Add(degree);
        context.Add(enrollment);
        context.Add(student);
        context.Add(tariff);
        await context.SaveChangesAsync();
        
        var debt = TestEntityGenerator.aDebtHistory(student.studentId!, true);
        debt.debtBalance = 80;
        debt.schoolyear = schoolyear.id!;
        debt.tariffId = tariff.tariffId;
        debt.tariff = tariff;
        context.Add(debt);
        await context.SaveChangesAsync();
    }
}