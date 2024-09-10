using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //Properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Content)
               .IsRequired()
               .HasMaxLength(500);

            //ForeignKeys
            builder.HasMany(x=>x.Comments).WithOne(x=>x.Post).HasForeignKey(x=>x.PostId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x=>x.Likes).WithOne(x=>x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.PostImages).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.PostType).WithMany(x=>x.Posts).HasForeignKey(x=>x.PostTypeId).OnDelete(DeleteBehavior.Restrict);
            //Indexes

            builder.HasIndex(x => x.Title);
            //builder.HasIndex(x=>x.PostTags);
            builder.HasIndex(x=>x.PostTypeId);
        }
    }
}
