using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC_App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public static void CreateInitialTestingTataBase(ApplicationDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.Migrate();


            // Save the TVShowLanguages
            context.SaveChanges();
        }
    }
}
