using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Projet_Entity_Framework_Code_First.DataBase.Models;

namespace Projet_Entity_Framework_Code_First.DataBase
{
        public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
        {
            public void Configure(EntityTypeBuilder<Blog> builder)
            {
                builder
                    .Property(b => b.Url)
                    .IsRequired();
            }
        }
}

