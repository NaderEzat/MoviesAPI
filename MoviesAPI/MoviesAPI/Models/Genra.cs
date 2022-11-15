using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Genra
    {
        public int Id { get; set; }
        [MaxLength]
        public string Name { get; set; } 

    }
}
