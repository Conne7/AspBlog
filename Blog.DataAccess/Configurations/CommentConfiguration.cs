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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            //Properties
            builder.Property(x => x.Text).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ParentId).IsRequired(false);
            

            //Foreign Keys
            builder.HasMany(x=>x.Likes).WithOne(x=>x.Comment).HasForeignKey(x=>x.CommentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Replies).WithOne(x => x.Parent).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);

            //Indexes
            builder.HasIndex(x => x.AuthorId);
            builder.HasIndex(x => x.PostId);


        }
    }
}
