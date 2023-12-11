namespace RadarWebAPI.Models
{
    public class PostsRequestModel
    {
        /// <summary>
        /// почта пользователя 
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// текст поста
        /// </summary>
        public string Description { get; set; }
    }
}
