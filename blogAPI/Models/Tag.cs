using MongoDB.Bson;

namespace blogAPI.Models
{
    public class Tag
    {
        public ObjectId Id { get; set; }
        public int UseAmount { get; set; } = 0;
        public string Context { get; set; } = string.Empty;
    }
}