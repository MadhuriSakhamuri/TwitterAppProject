using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TweetApp.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int LoginId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ContactNumber { get; set; }
        public string TweetId { get; set; }
        //public ObjectId Tweet { get; set; }
    }
}
