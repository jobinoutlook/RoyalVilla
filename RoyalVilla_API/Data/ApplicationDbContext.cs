using Microsoft.EntityFrameworkCore;

namespace RoyalVilla_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
