using Hadyach.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hadyach.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ArticleTag>().HasKey(sc => new { sc.ArticleId, sc.TagId });

            builder.Entity<ArticleTag>()
                .HasOne<Article>(sc => sc.Article)
                .WithMany(s => s.ArticleTags)
                .HasForeignKey(sc => sc.ArticleId);


            builder.Entity<ArticleTag>()
                .HasOne<Tag>(sc => sc.Tag)
                .WithMany(s => s.TagArticles)
                .HasForeignKey(sc => sc.TagId);
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleTag> ArticleTags { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
