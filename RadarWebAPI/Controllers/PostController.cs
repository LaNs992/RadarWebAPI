using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RadarWebAPI.DB;
using RadarWebAPI.Models;

namespace RadarWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyAllowSubdomainPolicy")]
    public class PostController:Controller
    {
        public DbContextOptions<AplicationContext> options = DBHelper.Option();

        [HttpGet("Read")]
        public List<ViewPostModel> GetPost()
        {
            using (var db = new AplicationContext(options))
            {
                return db.Posts.Join(db.Users,p=>p.UserId,u=>u.Id,(p,u)=>new ViewPostModel{Id=p.Id,Name=u.Name,Email=u.Email,Description=p.Description}).ToList();

            }
        }
        [HttpPost("Create")]
        public Posts NewPost([FromBody] PostsRequestModel postRequest)
        {
            

            using (var db = new AplicationContext(options))
            {
                var post = new Posts();
                post.Id = Guid.NewGuid();
                var user = db.Users.FirstOrDefault(x => x.Email == postRequest.Email);
                if (user == null) { return null; }
                post.UserId = user.Id;
                post.Description = postRequest.Description;
                db.Posts.Add(post);
                db.SaveChanges();
                return post;
            }
           
        }
        [HttpPut("Update")]
        public bool PostEdit ( PostWithIdRequestModel markerRequest)
        {         
            using (var db = new AplicationContext(options))
            {
                var posts = db.Posts.FirstOrDefault(x => x.Id == markerRequest.Id);
                if (posts != null)
                {
                    posts.Description = markerRequest.Description;
                    
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        [HttpGet("Read/{id}")]
        public List<ViewPostModel> GetPostId(Guid id)
        {
            
            using (var db = new AplicationContext(options))
            {
                var user= db.Users.FirstOrDefault(x => x.Id == id);

                if (user == null)
                {
                    return null;
                }
                return db.Posts
                    .OrderByDescending(x => x.UserId == id)
                    .Join(db.Users, p => p.UserId, u => u.Id, (p, u) => 
                    new ViewPostModel { Id = p.Id,
                        Name = u.Name,
                        Email = u.Email,
                        Description = p.Description
                    })
                    .ToList();

            }
        }
    }
}

