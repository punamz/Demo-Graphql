using demo_graphql.Domains.Services;
using HotChocolate.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using message_service.Domains.Entities;
using MongoDB.Driver;

namespace demo_graphql.Schemas.Queries;

public class UserQuery
{

    [UseOffsetPaging(DefaultPageSize = 10, IncludeTotalCount = true, ProviderName = "normal")]
    [UseProjection(Scope = "normal")]
    [UseFiltering(typeof(UserFilterType), Scope = "normal")]
    [UseSorting(typeof(UserSortingType), Scope = "normal")]
    public async Task<List<UserEntity>?> GetUsers([Service] IUserService userService)
    {
        return await userService.GetAllUser();
    }


    [UseOffsetPaging(DefaultPageSize = 10, IncludeTotalCount = true, ProviderName = "mongo")]
    [UseProjection(Scope = "mongo")]
    [UseFiltering(typeof(UserFilterType), Scope = "mongo")]
    [UseSorting(typeof(UserSortingType), Scope = "mongo")]
    public IExecutable<UserEntity> GetOffsetUsers([Service] IMongoDatabase database)
    {
        IMongoCollection<UserEntity> userCollection = database.GetCollection<UserEntity>("UsersTest");
        return userCollection.AsExecutable();
    }


    public async Task<UserEntity?> GetUsersById([Service] IUserService userService, [GraphQLName("userId")] int? userId)
    {
        return await userService.GetUser(userId);
    }
}


class UserFilterType : FilterInputType<UserEntity>
{
    protected override void Configure(IFilterInputTypeDescriptor<UserEntity> descriptor)
    {
        descriptor.Ignore(x => x.Id);
        descriptor.Field(x => x.UserId).Name("idUser");
        base.Configure(descriptor);
    }
}
class UserSortingType : SortInputType<UserEntity>
{
    protected override void Configure(ISortInputTypeDescriptor<UserEntity> descriptor)
    {
        descriptor.Ignore(x => x.Id);
        descriptor.Field(x => x.UserId).Name("idUser");
        base.Configure(descriptor);
    }
}