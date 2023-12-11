namespace RadarWebAPI.Models
{
    public class Posts
    {
        /// <summary>
        /// id пользователя
        /// id поста
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// имя пользователя 
        /// </summary>

        public Guid UserId { get; set; }

     
        /// <summary>
        /// текст поста
        /// </summary>
        public string Description { get; set; }

      
    }
}
