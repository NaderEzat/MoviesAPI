using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOS
{
    public class CreateGrnraDto
    {
        [MaxLength]
        public string Name { get; set; }
    }
}
