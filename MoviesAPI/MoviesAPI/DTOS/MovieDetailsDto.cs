using MoviesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOS
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }
        public string StoryLIne { get; set; }

        public byte[] Poster { get; set; }

        public int GenraId { get; set; }
        public string GenraName { get; set; }
    }
}
