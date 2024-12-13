using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;
using wsmcbl.src.exception;
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
        
        var tutor = new StudentTutorEntity("Tutor", "78451236", "tutorId00");
        context.Set<StudentTutorEntity>().Add(tutor);
        await context.SaveChangesAsync();
        
        var student = TestEntityGenerator.aStudent("std-00");
        student.tutorId = tutor.tutorId!;
        context.Set<StudentEntity>().Add(student);
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
    public async Task updateAsync_ShouldUpdateStudent_WhenStudentExist()
    {
        context = TestDbContext.getInMemory();
        
        var student = TestEntityGenerator.aStudent("std-00");
        context.Set<StudentEntity>().Add(student);
        await context.SaveChangesAsync();
        student.name = "New name";

        var sut = new StudentDaoPostgres(context);
        
        await sut.updateAsync(student);

        var existingStudent = await context.Set<StudentEntity>().FirstOrDefaultAsync(e => e.studentId == "std-00");
        Assert.Equal("New name", existingStudent!.name);
    }

    [Fact]
    public async Task updateAsync_ShouldThrowException_WhenStudentNotExist()
    {
        var student = TestEntityGenerator.aStudent("std-00");
        
        var sut = new StudentDaoPostgres(TestDbContext.getInMemory());

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.updateAsync(student));
    }
}