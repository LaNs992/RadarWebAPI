namespace RadarWebAPI.Models
{
    public class PostWithIdRequestModel : PostsRequestModel
    {
        public Guid Id { get; set; }
    }
}
