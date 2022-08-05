using AutoMapper;
using demo_graphql.Domains.Services;
using message_service.Domains.DTO;
using message_service.Domains.Entities;
using MongoDB.Driver;

namespace demo_graphql.Infrastructures.Repositories;

public class UserRepository : IUserService
{
    private readonly IMongoCollection<UserEntity> UserColection;

    private readonly IMapper Mapper;

    public UserRepository(IMongoDatabase database, IMapper map)
    {
        UserColection = database.GetCollection<UserEntity>("UsersTest");
        Mapper = map;
    }

    public async Task<UserEntity?> CreateUser(UserDTO user)
    {
        try
        {
            UserEntity userEntity = Mapper.Map<UserEntity>(user);
            await UserColection.InsertOneAsync(userEntity);
            return userEntity;

        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<List<UserEntity>?> GetAllUser()
    {
        try
        {
            var user = await UserColection.Find(_ => true).ToListAsync();
            return user;

        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<UserEntity?> GetUser(int? userId)
    {
        try
        {
            var user = await UserColection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return user;

        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<UserEntity?> UpdateUser(int userId, string newName)
    {
        try
        {
            await UserColection.UpdateOneAsync(x => x.UserId == userId, Builders<UserEntity>.Update.Set(z => z.FullName, newName));

            return await GetUser(userId);

        }
        catch (Exception e)
        {
            return null;
        }
    }
}
