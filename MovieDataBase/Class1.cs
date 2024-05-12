using Microsoft.EntityFrameworkCore;


namespace MovieDataBase
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cast { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
    }


    public class MovieData : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieData() : base("name=OOD_Exam_Jake")
        {
        }
    }
}
