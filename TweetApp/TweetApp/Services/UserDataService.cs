using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Models;

namespace TweetApp.Services
{
    public class UserDataService
    {
        private readonly IMongoCollection<UserData> _userDataCollection;
        private readonly IMongoCollection<Tweet> _tweetCollection;
        private readonly IMongoCollection<TweetMessages> _tweetMessagesCollection;
        public UserDataService(
        IOptions<TweetDataDatabaseSettings> tweetDataDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                tweetDataDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tweetDataDatabaseSettings.Value.DatabaseName);

            _userDataCollection = mongoDatabase.GetCollection<UserData>(
                tweetDataDatabaseSettings.Value.UserDataCollectionName);

            _tweetCollection = mongoDatabase.GetCollection<Tweet>(
                tweetDataDatabaseSettings.Value.TweetCollectionName);

            _tweetMessagesCollection = mongoDatabase.GetCollection<TweetMessages>(
                tweetDataDatabaseSettings.Value.TweetMessagesCollectionName);
        }

            public async Task<List<UserData>> GetAsync() =>
        await _userDataCollection.Find(_ => true).ToListAsync();

        public async Task<List<Tweet>> GetTweetAsync() =>
            await _tweetCollection.Find(_ => true).ToListAsync();

        public async Task<List<TweetMessages>> GetTweetMessagesAsync() =>
            await _tweetMessagesCollection.Find(_ => true).ToListAsync();

        public async Task<Tweet> GetTweetAsyncByUserName(string userName) =>
                await _tweetCollection.Find(x => x.HandleName == userName).FirstOrDefaultAsync();

        public async Task<List<TweetMessages>> GetTweetMessagesbyId(string tweetId)=>
            await _tweetMessagesCollection.Find(x => x.TweetId == tweetId).ToListAsync();
        public async Task<UserData?> GetAsync(string id) =>
                await _userDataCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<TweetMessages> GetTweetMessagesAsync(string id) =>
            await _tweetMessagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            public async Task CreateAsync(UserData newUserData) =>
                await _userDataCollection.InsertOneAsync(newUserData);

        public async Task CreateTweetAsync(TweetMessages tweetMessages) =>
                await _tweetMessagesCollection.InsertOneAsync(tweetMessages);

        public async Task UpdateAsync(string id, UserData updatedUserData) =>
                await _userDataCollection.ReplaceOneAsync(x => x.Id == id, updatedUserData);

        public async Task UpdateTweetAsync(string id, TweetMessages updatedTweetMessages) =>
            await _tweetMessagesCollection.ReplaceOneAsync(x => x.Id == id, updatedTweetMessages);

        public async Task UpdateLikeAsync(string id, TweetMessages updatedTweetMessages) =>
            await _tweetMessagesCollection.ReplaceOneAsync(x => x.Id == id, updatedTweetMessages);

        public async Task RemoveAsync(string id) =>
                await _userDataCollection.DeleteOneAsync(x => x.Id == id);

        public async Task RemoveTweetAsync(string id) =>
                await _tweetMessagesCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<UserData?> Find(Login login) =>
            await _userDataCollection.Find(userLogin => userLogin.Email == login.Email && userLogin.Password==login.Password).FirstOrDefaultAsync();
        
    }
}
