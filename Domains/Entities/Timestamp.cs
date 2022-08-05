using MongoDB.Bson.Serialization.Attributes;

namespace demo_graphql.Domains.Entities;

public class Timestamp
{
    [BsonElement("createAt")]
    public DateTime CreateAt { get; set; } = DateTime.Now;
    [BsonElement("updateAt")]
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}
