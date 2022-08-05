using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using message_service.Domains.Entities;

namespace demo_graphql.Schemas.Subcriptions;

public class UserSupscription
{
    [Subscribe]
    public UserEntity UserCreadted([EventMessage] UserEntity user) => user; 
    [Subscribe]
    public UserEntity UserUpdated([EventMessage] UserEntity user) => user;
    [SubscribeAndResolve]
    public async ValueTask<ISourceStream<string>> UserDeletedAsync([Service] ITopicEventReceiver topicEventReceiver, string id)
    {
        var topic = $"{id}_{nameof(UserSupscription.UserDeletedAsync)}";
        return await topicEventReceiver.SubscribeAsync<string, string>(topic);
    }
}
