using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace S00234729_Sample_Exam
{
    public class MovieData : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public MovieData(string fullName) : base(fullName)
        {
           
        }
    }
}
