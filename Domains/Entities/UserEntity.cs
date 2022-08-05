
using demo_graphql.Domains.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace message_service.Domains.Entities;

public class UserEntity : Timestamp
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("userId")]
    public long UserId { get; set; } = 0;
    [BsonElement("email")]
    public string Email { get; set; } = null!;
    [BsonElement("fullName")]
    public string FullName { get; set; } = null!;
    [BsonElement("avatar")]
    public string? Avatar { get; set; }
    [BsonElement("phongBanID")]
    public long PhongBanID { get; set; } = 0;
    [BsonElement("tenPhongBan")]
    public string? TenPhongBan { get; set; }
    [BsonElement("chucVuID")]
    public long ChucVuID { get; set; } = 0;
    [BsonElement("tenChucVu")]
    public string? TenChucVu { get; set; }
    [BsonElement("address")]
    public string? Address { get; set; }
    [BsonElement("phoneNumber")]
    public string? PhoneNumber { get; set; }
    [BsonElement("gender")]
    public string? Gender { get; set; } = "male";
    [BsonElement("isAdmin")]
    public bool IsAdmin { get; set; } = false;
    [BsonElement("lastOnline")]
    public DateTime? LastOnline { get; set; }
}
