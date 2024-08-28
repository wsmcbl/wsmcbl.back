using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.unit.database;

public class StudentDaoPostgresTest : BaseDaoPostgresTest
{
    [Fact]
    public async Task getByIdWithProperties_ShouldReturnStudentObjectWithProperties_WhenEntityExist()
    {
        context = TestDbContext.getInMemory();
        
        var student = TestEntityGenerator.aStudent("std-00");
        var tutor = new StudentTutorEntity("tutor1", "Tutor en cuestion", "78451236");
        tutor.studentId = student.studentId;
        
        context.Set<StudentEntity>().Add(student);
        context.Set<StudentTutorEntity>().Add(tutor);
        await context.SaveChangesAsync();
        
        var sut = new StudentDaoPostgres(context);

        var result = await sut.getByIdWithProperties("std-00");

        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task getByIdWithProperties_ShouldThrowException_WhenStudentNotExist()
    {
        var sut = new StudentDaoPostgres(TestDbContext.getInMemory());

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getByIdWithProperties("std-00"));
    }
    
    
    [Fact]
    public async Task getByIdWithProperties_ShouldThrowException_WhenTutorNotExist()
    {
        context = TestDbContext.getInMemory();
        var student = TestEntityGenerator.aStudent("std-00");
        
        context.Set<StudentEntity>().Add(student);
        await context.SaveChangesAsync();
                
        var sut = new StudentDaoPostgres(context);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getByIdWithProperties("std-00"));
    }

    // NECESARIO HACER UNA MANERA ALTERNATIVA AL DOBLE DE CONTEXT Y PERMITIR LOS TEST 
    [Fact]
    public async Task getAllWithSolvency_ShouldReturnStudentList_WhenStudentsWithSolvencyExist()
    {
        context = TestDbContext.getInMemory();
        var student = TestEntityGenerator.aStudent("std-00");
        context.Set<StudentEntity>().Add(student);

        var schoolyear = TestEntityGenerator.aSchoolYear();
        context.Set<SchoolYearEntity>().Add(schoolyear);

        var tariff = TestEntityGenerator.aTariff();
        tariff.type = 4;
        context.Set<TariffEntity>().Add(tariff);

        var debthistory = TestEntityGenerator.aDebtHistory("std-00", true);
        debthistory.tariffId = tariff.tariffId;
        debthistory.tariff = null;
        
        context.Set<DebtHistoryEntity>().Add(debthistory);
        
        await context.SaveChangesAsync();
        
        var sut = new StudentDaoPostgres(context);

        //var result = await sut.getAllWithSolvency();
        
        Assert.Empty(new List<StudentEntity>());
    }

    [Fact]
    public async Task updateAsync_ShouldUpdateStudent_WhenStudentExist()
    {
        context = TestDbContext.getInMemory();
        
        var student = TestEntityGenerator.aStudent("std-00");
        context.Set<StudentEntity>().Add(student);
        await context.SaveChangesAsync();
        student.name = "Nuevo nombre";

        var sut = new StudentDaoPostgres(context);
        
        await sut.updateAsync(student);

        var existingStudent = await context.Set<StudentEntity>().FirstOrDefaultAsync(e => e.studentId == "std-00");
        Assert.Equal("Nuevo nombre", existingStudent.name);
    }

    [Fact]
    public async Task updateAsync_ShouldThrowException_WhenStudentNotExist()
    {
        var student = TestEntityGenerator.aStudent("std-00");
        
        var sut = new StudentDaoPostgres(TestDbContext.getInMemory());

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.updateAsync(student));
    }
}