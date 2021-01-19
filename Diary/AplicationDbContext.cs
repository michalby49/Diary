using Diary.Model.Configurations;
using Diary.Model.Domains;
using Diary.Model.Wrappers;
using Diary.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext()
            : base($@"Server=({ServerWrapper.Address})\{ServerWrapper.Name};Database={ServerWrapper.DataBaseName};User Id={ServerWrapper.DataBaseLogin};Password={ServerWrapper.DataBasePassword};App=EntityFramework")
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