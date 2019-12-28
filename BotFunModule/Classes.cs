namespace BotFunModule
{
    public struct RedditResponse
    {
        public RedditPost[] data;
    }

    public struct RedditPost
    {
        public long id { get; set; }
        public string hash { get; set; }
        public string ext { get; set; }

        public string title { get; set; }
        public string author { get; set; }
        public long views { get; set; }
    }
}