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

        
    }

    
}