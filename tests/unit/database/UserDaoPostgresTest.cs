using Microsoft.EntityFrameworkCore;
using NSubstitute;
using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class UserDaoPostgresTest : BaseDaoPostgresTest
{
    private UserDaoPostgres? sut;

    [Fact]
    public async Task create_ShouldCreateUser()
    {
        context = TestDbContext.getInMemory();
        var user = TestEntityGenerator.aUser();

        sut = new UserDaoPostgres(context);
        
        sut.create(user);
        await context.SaveChangesAsync();
        
        var userCreated = await context.Set<UserEntity>()
            .Where(t => t.name == user.name).ToListAsync();

        Assert.NotNull(userCreated);
        Assert.Equal(user, userCreated[0]);
    }

    
    [Fact]
    public async Task getById_ShouldReturnsUser_WhenUserExist()
    {
        var user = TestEntityGenerator.aUser();

        context = TestDbContext.getInMemory();
        context.Set<UserEntity>().Add(user);
        await context.SaveChangesAsync();

        sut = new UserDaoPostgres(context);
        
        var result = await sut.getById((Guid)user.userId!);

        Assert.NotNull(result);
        Assert.Equal(user, result);
    }
    
    [Fact]
    public async Task getById_ShouldReturnsNull_WhenUserNotExist()
    {
        context = TestDbContext.getInMemory();

        sut = new UserDaoPostgres(context);
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getById("user-1"));
    }


    [Fact]
    public async Task update_UserUpdate()
    {
        var user = TestEntityGenerator.aUser();

        var userEntities = TestDbSet<UserEntity>.getFake([user]);
        context.Set<UserEntity>().Returns(userEntities);

        sut = new UserDaoPostgres(context);
        
        sut.update(user);
        
        var userCreated = await context.Set<UserEntity>()
            .Where(t => t.userId == user.userId!).ToListAsync();

        Assert.NotNull(userCreated);
        Assert.Equal(user, userCreated[0]);
    }


    [Fact]
    public async Task getAll_ReturnsList()
    {
        List<UserEntity> list = [ TestEntityGenerator.aUser() ];

        var entities = TestDbSet<UserEntity>.getFake(list);
        context.Set<UserEntity>().Returns(entities);
        sut = new UserDaoPostgres(context);

        var result = await sut.getAll();

        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
    
    [Fact]
    public async Task getAll_ReturnsEmptyList()
    {
        var entities = TestDbSet<UserEntity>.getFake([]);
        context.Set<UserEntity>().Returns(entities);
        
        sut = new UserDaoPostgres(context);

        var result = await sut.getAll();

        Assert.Empty(result);
    }
}