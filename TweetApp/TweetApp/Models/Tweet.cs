using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TweetApp.Models
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string TweetId { get; set; }
        public string HandleName { get; set; }
        //public string TweetMessageId { get; set; }
        //public BsonArray UserData { get; set; }
        //public BsonArray TweetMessages { get; set; }
        //BsonDateTime date = BsonDateTime.Now();
        //public DateTime DateTime { get; set; } = DateTime.Now;
        //public string Time { get; set; }
        //public ImageSource profilePicture { get; set; }
    }
}
