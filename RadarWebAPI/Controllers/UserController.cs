using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadarWebAPI.DB;
using RadarWebAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace RadarWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyAllowSubdomainPolicy")]
    public class UserController : Controller
    {
        public DbContextOptions<AplicationContext> options = DBHelper.Option();

        [HttpGet("Read")]
        public List<User> GetUser()
        {
            using (var db = new AplicationContext(options))
            {
                return db.Users.ToList();
            }
        }
     
        [HttpPost("Create")]
        public User NewUser([FromBody] UserRequestModel userRequest)
        {
            var user = new User();
           user.Id=Guid.NewGuid();
            user.Name = userRequest.Name;
            user.Email = userRequest.Email;
            user.Password=GetHash.getHashSha256(userRequest.Password);


            using (var db = new AplicationContext(options))
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return user;
        }
        [HttpPut("Update")]
        public bool UserEdit(UserWithIdRequestModel userRequest)
        {    
            using (var db = new AplicationContext(options))
            {
               var user = db.Users.FirstOrDefault(x => x.Id == userRequest.Id);
                if (user != null)
                {
                    
                    user.Name = userRequest.Name;
                    user.Email = userRequest.Email;
                    user.Password=GetHash.getHashSha256(userRequest.Password);
                    db.SaveChanges();
                    return true;
                }
                return false;

            }
        }
    }
}
