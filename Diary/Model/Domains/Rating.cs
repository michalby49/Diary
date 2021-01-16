using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Model.Domains
{
    public class Rating
    {
        public Rating()
        {
            Ratings = new Colletion<Rating>();
        }
        public int Id { get; set; }
        public int Rate { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public Student Student { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
