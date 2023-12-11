using Microsoft.EntityFrameworkCore.Design;

namespace RadarWebAPI.DB
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<AplicationContext>
    {
        public AplicationContext CreateDbContext(string[] args)
         => new AplicationContext(DBHelper.Option());

    }
}
