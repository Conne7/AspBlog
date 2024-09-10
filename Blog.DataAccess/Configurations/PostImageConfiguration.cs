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
    public class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.Property(x => x.Image).IsRequired(false).HasDefaultValue("defaultPostPic.jpg").HasMaxLength(50);

            builder.Property(x => x.Alt).IsRequired(false).HasDefaultValue("defaultPostPic").HasMaxLength(50);
        }
    }
}
