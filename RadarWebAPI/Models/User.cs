using RadarWebAPI.Models.Enums;

namespace RadarWebAPI.Models
{/// <summary>
 /// Сущность пользователя
 /// </summary>
    public class User
    {
        /// <summary>
        /// id пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// имя пользователя 
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// почта пользощавтеля 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// роль пользователя (user,admin)
        /// </summary>
        public RolesType  Roles { get; set; }=RolesType.User;

     
        
    }
}
