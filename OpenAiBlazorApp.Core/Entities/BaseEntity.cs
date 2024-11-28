using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OpenAiBlazorApp.Core.Entities;
public abstract class BaseEntity
{
    private string? _id;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id
    {
        get => _id;
        set
        {
            if (string.IsNullOrEmpty(value) || !ObjectId.TryParse(value, out _))
            {
                _id = ObjectId.GenerateNewId().ToString();
            }
            else
            {
                _id = value;
            }
        }
    }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
