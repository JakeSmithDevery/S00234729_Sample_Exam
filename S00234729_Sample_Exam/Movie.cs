using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace S00234729_Sample_Exam
{
    //Class required for 1(d)
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Cast { get; set; }
        
        //part of one to many (to many)
        public virtual ICollection<Booking> Bookings { get; set; }
    }


}
