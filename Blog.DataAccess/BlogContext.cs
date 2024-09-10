using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess
{
    public class BlogContext : DbContext
    {
        private readonly string _connectionString;
        public BlogContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BlogContext()
        {
            _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Blog;TrustServerCertificate=true;Integrated security = true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<PostType>().HasKey(x => x.Type);
            modelBuilder.Entity<PostType>().Property(x => x.Name).IsRequired().HasMaxLength(32);

            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId });

            //modelBuilder.Entity<Entity>().Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            modelBuilder.Entity<Tag>().Property(x => x.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Tag>().HasIndex(x => x.Name);
            modelBuilder.Entity<Tag>().Property(x => x.Description).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<ErrorLog>().HasKey(x=>x.ErrorId);

            modelBuilder.Entity<PostLike>().HasKey(x => new { x.UserId, x.PostId });




            // Mapiraj tabelu na klasu
            modelBuilder.Entity<UseCaseLog>(entity =>
            {
                entity.HasNoKey(); // Ako tabela nema primarni ključ
                entity.ToTable("UseCaseLogs", t => t.ExcludeFromMigrations()); // Sprečava kreiranje šeme za tabelu

                entity.Property(e => e.LogId).HasColumnName("LogId");
                entity.Property(e => e.UseCaseName).HasColumnName("UseCaseName");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.Data).HasColumnName("Data");
                entity.Property(e => e.ExecutedAt).HasColumnName("ExecutedAt");
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.IsActive = true;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
        }

        //Posts
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostType> PostTypes { get; set; }

        //Users
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }



        //Comments
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        //Other
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public DbSet<UseCaseLog> UseCaseLogs { get; set; }

    }
}
