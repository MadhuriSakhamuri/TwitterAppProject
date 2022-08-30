using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Runtime.Serialization;

namespace TweetApp.Models
{
    public class TweetMessages
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [IgnoreDataMember]
        [HiddenInput(DisplayValue = false)]
        [SwaggerSchema(ReadOnly = true)]

        public string? Id { get; set; }
        public string TweetId { get; set; }
        //public string TweetMessageId { get; set; }
        public string TweetMessage { get; set; }

        [IgnoreDataMember]
        [HiddenInput(DisplayValue = false)]
        [SwaggerSchema(ReadOnly = true)]
        public string Time { get; set; } = DateTime.Now.ToString($"ddd MMM dd yyyy hh:mm:ss ")+ConfigConstants.timeZone;

        public int Like { get; set; }
        //public string Time { get; set; }= Convert.ToString(TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToLocalTime(),
        //TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")));
        //public string Time { get; set; } = Convert.ToString(DateTime.Now);
    }
}
