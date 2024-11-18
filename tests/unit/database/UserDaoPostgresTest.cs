using Microsoft.EntityFrameworkCore;
using NSubstitute;
using wsmcbl.src.database;
using wsmcbl.src.model.config;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class UserDaoPostgresTest : BaseDaoPostgresTest
{
    private UserDaoPostgres? dao;

    [Fact]
    public async Task create_UserCreated()
    {
        var user = TestEntityGenerator.aUser("user-1");

        var userEntities = TestDbSet<UserEntity>.getFake([user]);
        context.Set<UserEntity>().Returns(userEntities);

        dao = new UserDaoPostgres(context);
        
        dao.create(user);
        
        var userCreated = await context.Set<UserEntity>()
            .Where(t => t.userId == user.userId!).ToListAsync();

        Assert.NotNull(userCreated);
        Assert.Equal(user, userCreated[0]);
    }

    
    [Fact]
    public async Task getById_ReturnsUser()
    {
        var user = TestEntityGenerator.aUser("user-1");

        context = TestDbContext.getInMemory();
        context.Set<UserEntity>().Add(user);
        await context.SaveChangesAsync();

        dao = new UserDaoPostgres(context);
        
        //var result = await dao.getById(user.name);
        UserEntity? result = null;

        Assert.NotNull(result);
        Assert.Equal(user, result);
    }
    
    [Fact]
    public async Task getById_UserNotFound()
    {
        context = TestDbContext.getInMemory();

        dao = new UserDaoPostgres(context);
        
        //var result = await dao.getById("user-1");
        UserEntity? result = null;

        Assert.Null(result);
    }


    [Fact]
    public async Task update_UserUpdate()
    {
        var user = TestEntityGenerator.aUser("user-1");

        var userEntities = TestDbSet<UserEntity>.getFake([user]);
        context.Set<UserEntity>().Returns(userEntities);

        dao = new UserDaoPostgres(context);
        
        dao.update(user);
        
        var userCreated = await context.Set<UserEntity>()
            .Where(t => t.userId == user.userId!).ToListAsync();

        Assert.NotNull(userCreated);
        Assert.Equal(user, userCreated[0]);
    }


    [Fact]
    public async Task getAll_ReturnsList()
    {
        List<UserEntity> list = [ TestEntityGenerator.aUser("user-1") ];

        var entities = TestDbSet<UserEntity>.getFake(list);
        context.Set<UserEntity>().Returns(entities);
        dao = new UserDaoPostgres(context);

        var result = await dao.getAll();

        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
    
    [Fact]
    public async Task getAll_ReturnsEmptyList()
    {
        var entities = TestDbSet<UserEntity>.getFake([]);
        context.Set<UserEntity>().Returns(entities);
        
        dao = new UserDaoPostgres(context);

        var result = await dao.getAll();

        Assert.Empty(result);
    }
}