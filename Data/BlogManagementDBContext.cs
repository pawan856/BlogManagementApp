using BlogManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementApp.Data
{
    public class BlogManagementDBContext : DbContext
    {
        public BlogManagementDBContext(DbContextOptions<BlogManagementDBContext> options)
            : base(options)
        {
        }

        // Configure model relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure Slug is unique
            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.Slug)
                .IsUnique();

            // Cascade delete comments when a blog post is deleted
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Authors
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "C#" },
                new Category { Id = 2, Name = "ASP.NET Core" },
                new Category { Id = 3, Name = "SQL Server" },
                new Category { Id = 4, Name = "Java" }
                //You can add more categories as needed by extending the HasData method
            );

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Pranaya Rout", Email = "Pranaya.Rout@example.com" },
                new Author { Id = 2, Name = "Rakesh Kumar", Email = "Rakesh.Kumar@example.com" },
                new Author { Id = 3, Name = "Hina Sharma", Email = "Hina.Sharma@example.com" }
                //You can add more authors as needed by extending the HasData method
            );
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}