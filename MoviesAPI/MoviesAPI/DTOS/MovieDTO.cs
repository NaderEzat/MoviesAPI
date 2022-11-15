using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOS
{
    public class MovieDTO
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLIne { get; set; }

        public IFormFile? Poster { get; set; }

        public int GenraId { get; set; }    
    }
}
