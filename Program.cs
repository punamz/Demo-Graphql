

using AutoMapper;
using demo_graphql.Common;
using demo_graphql.Domains.Services;
using demo_graphql.Infrastructures.Repositories;
using demo_graphql.Schemas.Mutations;
using demo_graphql.Schemas.Queries;
using demo_graphql.Schemas.Subcriptions;
using message_service.Domains.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

var Username = Environment.GetEnvironmentVariable("USERNAME");
var Password = Environment.GetEnvironmentVariable("PASSWORD");
var DatabaseUrl = Environment.GetEnvironmentVariable("BASE_URL");
var Port = Environment.GetEnvironmentVariable("PORT");
var DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");


var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
    mc.AllowNullCollections = true;
    mc.AllowNullDestinationValues = true;
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient(x =>
{
    string connectionString = $"mongodb://{Username}:{Password}@{DatabaseUrl}:{Port}/";
    var mongoConnectionUrl = new MongoUrl(connectionString);
    var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
    mongoClientSettings.ClusterConfigurator = cb =>
    {
        // This will print the executed command to the console
        cb.Subscribe<CommandStartedEvent>(e => Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}"));
        cb.Subscribe<CommandSucceededEvent>(e => Console.WriteLine($"{e.CommandName} - {e.Timestamp}  - {e.Duration} \n ----------------------------------------------------------------------"));
    };
    var client = new MongoClient(mongoClientSettings);
    return client.GetDatabase(DatabaseName);    
});


//builder.Services.AddTransient(x => new MongoClient($"mongodb://{Username}:{Password}@{DatabaseUrl}:{Port}/").GetDatabase(DatabaseName));
builder.Services.AddScoped<IUserService, UserRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddGraphQLServer()
    .AddQueryType<UserQuery>()
    .AddMutationType<UserMutation>()
    .AddSubscriptionType<UserSupscription>()
    .AddFiltering("normal")
    .AddSorting("normal")
    .AddProjections("normal")
    .AddQueryableCursorPagingProvider("normal")
    .AddMongoDbFiltering("mongo")
    .AddMongoDbSorting("mongo")
    .AddMongoDbProjections("mongo")
    .AddMongoDbPagingProviders("mongo");


builder.Services.AddInMemorySubscriptions();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", async (IUserService playerService) =>
{
    return await playerService.GetAllUser();
});

app.MapGet("/getuser", async (IUserService playerService, IMongoDatabase database,int userId) =>
{
    IMongoCollection<UserEntity> UserColection = database.GetCollection<UserEntity>("UsersTest");

    return await UserColection.Find(x => x.UserId == userId).SortBy(x=>x.FullName).Skip(0).Limit(3).ToListAsync();

});




app.MapGraphQL();
app.UseWebSockets();
//app.UseGraphQLAltair();
app.Run();
