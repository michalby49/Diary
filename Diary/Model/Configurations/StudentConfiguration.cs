using Diary.Model.Domains;
using System.Data.Entity.ModelConfiguration;

namespace Diary.Model.Configurations
{
    class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");

            HasKey(x => x.Id);
        }

    }
}
