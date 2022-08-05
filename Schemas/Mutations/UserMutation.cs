using demo_graphql.Domains.Services;
using demo_graphql.Schemas.Subcriptions;
using HotChocolate.Subscriptions;
using message_service.Domains.DTO;
using message_service.Domains.Entities;
using MongoDB.Bson;

namespace demo_graphql.Schemas.Mutations;

public class UserMutation
{
    public async Task<UserEntity?> CreateUsers([GraphQLName("userabs")] UserDTO user, [Service] ITopicEventSender topicEventSender, [Service] IUserService userService)
    {

        UserEntity? userEntity = await userService.CreateUser(user);

        if (userEntity != null)
            await topicEventSender.SendAsync(nameof(UserSupscription.UserCreadted), userEntity);


        return userEntity;
    }
    public async Task<UserEntity?> UpdateUsers([Service] IUserService userService, [Service] ITopicEventSender topicEventSender, int userId, string newName)
    {


        var userEntity = await userService.UpdateUser(userId, newName);
        if (userEntity == null)
            throw new GraphQLException(new Error("User not found", "INVALID_USER"));

        await topicEventSender.SendAsync(nameof(UserSupscription.UserUpdated), userEntity);

        return userEntity;
    }
    public async Task<bool?> DeleteUsers([Service] IUserService userService, [Service] ITopicEventSender topicEventSender, int userId)
    {
        var userEntity = await userService.GetUser(userId);

        if (userEntity == null)
            throw new GraphQLException(new Error("User not found", "INVALID_USER"));

        string topic = $"{userEntity.Id}_{nameof(UserSupscription.UserDeletedAsync)}";
        await topicEventSender.SendAsync(topic, userEntity.Id);
        return true;
    }
}
