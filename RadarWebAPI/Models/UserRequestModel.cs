using RadarWebAPI.Models.Enums;

namespace RadarWebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRequestModel
    {
        public string Name { get; set; }

        /// <summary>
        /// почта пользощавтеля 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// пароль пользователя
        /// </summary>
        public string Password { get; set; }

        
       

    }
}
