using Microsoft.EntityFrameworkCore;

namespace RadarWebAPI.DB
{
    public class DBHelper
    {
        public static DbContextOptions<AplicationContext> Option()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AplicationContext>();
            return optionsBuilder
                    .UseSqlServer(connectionString)
                    .Options;
        }
    }
}
