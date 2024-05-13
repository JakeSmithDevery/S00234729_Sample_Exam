using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace S00234729_Sample_Exam
{

    //Booking class for Q1(c)
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfTickets { get; set; }

        public int MovieID { get; set; }

        //part of one to many (to one)
        public virtual Movie Movie { get; set; }
    }
}
