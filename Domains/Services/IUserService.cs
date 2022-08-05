using message_service.Domains.DTO;
using message_service.Domains.Entities;

namespace demo_graphql.Domains.Services;

public interface IUserService
{
    Task<List<UserEntity>?> GetAllUser();
    Task<UserEntity?> GetUser(int? userId);
    Task<UserEntity?> CreateUser(UserDTO user);
    Task<UserEntity?> UpdateUser(int userId, string fullname);
}
