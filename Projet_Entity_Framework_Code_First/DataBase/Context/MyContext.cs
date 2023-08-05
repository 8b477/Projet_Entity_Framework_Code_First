
using Microsoft.EntityFrameworkCore;

using Projet_Entity_Framework_Code_First.DataBase.Models;

namespace Projet_Entity_Framework_Code_First.DataBase
{
    public partial class MyContext : DbContext
    {
                
        public MyContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
        }

        public DbSet<Blog> Blog { get; set; }
    }
}

