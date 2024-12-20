using Application_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
    // كلاس  من اجل الكيانات الخاصة بالمشروع 
    public class AppDbContext : DbContext
    {
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<TVShowLanguages> TVShowLanguages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Ahmed;Integrated Security=True;Trust Server Certificate=True";

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
        public static void CreateInitialTestingTataBase(AppDbContext context)
        {
            // للتأكد من ان قاعدة البيانات تم انشاءها 
            context.Database.EnsureCreated();
            // اضافة لغات الى المشروع 
            if (!context.Languages.Any())
            {

                context.Languages.Add(new Languages { Name = "Arabic" });
                context.Languages.Add(new Languages { Name = "English" });
                context.Languages.Add(new Languages { Name = "Portuguese" });
                context.Languages.Add(new Languages { Name = "Spanish" });
                context.Languages.Add(new Languages { Name = "French" });
                context.Languages.Add(new Languages { Name = "German" });
                context.Languages.Add(new Languages { Name = "Japanese" });
                context.Languages.Add(new Languages { Name = "Korean" });
                context.Languages.Add(new Languages { Name = "Russian" });
                context.Languages.Add(new Languages { Name = "Italian" });
                context.Languages.Add(new Languages { Name = "Chinese" });
                context.SaveChanges();
            }

            //  الى القاعدة في حال كانت فارغة قاعدة البيانات Attachment اضافة 
            if (!context.Attachments.Any(a => a.Title == "test1"))
            {
                context.Attachments.Add(new Attachment { FileType = FileType.Jpg, Path = "/uploads/img.jpg", Title = "test1" });
                context.SaveChanges();
            }

            //  الى قاعدة البيانات في حال كانت فارغة قاعدة البيانات  TVShow اضافة 
            if (!context.TVShows.Any(ts => ts.Title == "first tvshow"))
            {
                var tvShow = new TVShow
                {
                    IsActive = true,
                    Title = "first tvshow",
                    Rating = Rating.PG13,
                    ReleaseDate = new DateTime(2024, 1, 1),
                    URL = "https://youtu.be/cYDu2RTk5q4?si=se1WMIvVkogUD9KO",
                    AttachmentId = context.Attachments.FirstOrDefault(a => a.Title == "test1")?.AttachmentId ?? 0
                };
                context.TVShows.Add(tvShow);
                context.SaveChanges();

                // اضافة لغة لبرنامج 
                var tvShowId = tvShow.TVShowId;

                context.TVShowLanguages.Add(new TVShowLanguages { LanguageId = context.Languages.FirstOrDefault(l => l.Name == "Arabic")?.LanguageId ?? 0, TVShowId = tvShowId , LanguageName = "Arabic"});
                context.TVShowLanguages.Add(new TVShowLanguages { LanguageId = context.Languages.FirstOrDefault(l => l.Name == "English")?.LanguageId ?? 0, TVShowId = tvShowId , LanguageName ="English" });
                context.SaveChanges();
            }
        }

    }
}
