namespace TweetApp.Models
{
    public class TweetDataDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserDataCollectionName { get; set; } = null!;
        public string TweetCollectionName { get; set; } = null!;
        public string TweetMessagesCollectionName { get; set; } = null!;

    }
}
