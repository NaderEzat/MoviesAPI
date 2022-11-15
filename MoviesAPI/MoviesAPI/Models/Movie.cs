using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
         public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLIne{ get; set; }

        public byte[] Poster { get; set; }

        public int GenraId { get; set; }
        public Genra Genra { get; set; }


    }
}
