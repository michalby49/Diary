using Diary.Model.Configurations;
using Diary.Model.Domains;
using Diary.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext()
            : base($@"Server=({ServerAdres})\{ServerName};Database={DataBaseName};User Id={DataBaseLogin};Password={DataBesePassword};App=EntityFramework")
        {   
        }

        public static string ServerAdres
        {
            get
            {
                return Settings.Default.ServerAdres;
            }
            set
            {
                Settings.Default.ServerAdres = value;
            }  
        }
        public static string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public static string DataBaseName
        {
            get
            {
                return Settings.Default.DataBaseName;
            }
            set
            {
                Settings.Default.DataBaseName = value;
            }
        }
        public static string DataBaseLogin
        {
            get
            {
                return Settings.Default.DataBaseLogin;
            }
            set
            {
                Settings.Default.DataBaseLogin = value;
            }
        }
        public static string DataBesePassword
        {
            get
            {
                return Settings.Default.DataBesePassword;
            }
            set
            {
                Settings.Default.DataBesePassword = value;
            }
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