using Microsoft.EntityFrameworkCore;

namespace mobile_api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> configuration) : base(configuration)
        {
            
        }
    }
}
