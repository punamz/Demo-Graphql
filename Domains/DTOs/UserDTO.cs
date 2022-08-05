using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace message_service.Domains.DTO;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class UserDTO
{
    public string? Id { get; set; } = null!;
    public long? UserId { get; set; } = 0;
    public string? Email { get; set; } = null!;
    public string? FullName { get; set; } = null!;
    public string? Avatar { get; set; }
    public long? PhongBanID { get; set; } = 0;
    public string? TenPhongBan { get; set; }
    public long? ChucVuID { get; set; } = 0;
    public string? TenChucVu { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; } = "male";
    public bool? IsAdmin { get; set; } = false;
    public DateTime? LastOnline { get; set; }
}
