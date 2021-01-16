using Diary.Model.Configurations;
using Diary.Model.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace Diary
{
    public class AplicationDbContext : DbContext
    {
       
        public AplicationDbContext()
            : base("name=AplicationDbContext")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());

        }

    }

    
}