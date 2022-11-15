using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOS;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDBcontext _dbcontext;
        private new List<string> _allowedExtentions = new List<string> { ".jpg", ".png" };
        private long _MaxAllowedPosterSize = 1048576;

        public MoviesController(AppDBcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Get All Movies
        [HttpGet]
        public async Task<IActionResult> GetAllMovie()
        {
            var movies = await _dbcontext.Movies.OrderByDescending(x => x.Rate).Include(a => a.Genra).Select(m => new MovieDetailsDto
            {
                Id = m.Id,
                GenraId = m.GenraId,
                GenraName = m.Genra.Name,
                Poster = m.Poster,
                Rate = m.Rate,
                Title = m.Title,
                StoryLIne = m.StoryLIne,
                Year = m.Year
            })
            .ToListAsync();
            return Ok(movies);
        }
        #endregion

        #region Get Movie By Id 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _dbcontext.Movies.Include(a => a.Genra).SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)

                return NotFound();
            var dto = new MovieDetailsDto
            {
                Id = movie.Id,
                GenraId = movie.GenraId,
                GenraName = movie.Genra.Name,
                Poster = movie.Poster,
                Rate = movie.Rate,
                Title = movie.Title,
                StoryLIne = movie.StoryLIne,
                Year = movie.Year

            };

            return Ok(dto);
        }
        #endregion

        #region Get All Movies By One Genre Id
        [HttpGet("GetByGenraId")]

        public async Task<IActionResult> GetByGenraId(int GenraId)
        {
            var movies = await _dbcontext.Movies.Where(i => i.GenraId == GenraId).OrderByDescending(x => x.Rate).Include(a => a.Genra).Select(m => new MovieDetailsDto
            {
                Id = m.Id,
                GenraId = m.GenraId,
                GenraName = m.Genra.Name,
                Poster = m.Poster,
                Rate = m.Rate,
                Title = m.Title,
                StoryLIne = m.StoryLIne,
                Year = m.Year
            })
          .ToListAsync();
            return Ok(movies);

        }
        #endregion

        #region Create New Movie

        [HttpPost]
        public async Task<IActionResult> Ceatemovie([FromForm] MovieDTO movieDTO)
        {
            if (!_allowedExtentions.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                return BadRequest("Only .png & .jpg image are allow");
            if (movieDTO.Poster.Length > _MaxAllowedPosterSize)
                return BadRequest("Allow max size Foe Poter 1 Mb");

            var IsVaildGenra = await _dbcontext.Genras.AnyAsync(g => g.Id == movieDTO.GenraId);
            if (!IsVaildGenra)
                return BadRequest("inavil GenraId");

            using var datastreem = new MemoryStream();
            await movieDTO.Poster.CopyToAsync(datastreem);

            var movie = new Movie
            {
                GenraId = movieDTO.GenraId,
                Title = movieDTO.Title,
                StoryLIne = movieDTO.StoryLIne,
                Poster = datastreem.ToArray(),
                Rate = movieDTO.Rate,
                Year = movieDTO.Year,
            };
            await _dbcontext.AddAsync(movie);
            _dbcontext.SaveChanges();
            return Ok(movie);
        }
        #endregion

        #region Update Movie 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id , [FromForm] MovieDTO movieDTO)
        {
            var movie = await _dbcontext.Movies.FindAsync(id);
            if (movie == null)
                return NotFound($"NoMovie with Id =  {id} ");

            var IsVaildGenra = await _dbcontext.Genras.AnyAsync(g => g.Id == movieDTO.GenraId);
            if (!IsVaildGenra)
                return BadRequest("inavil GenraId");

            if(movieDTO.Poster != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                    return BadRequest("Only .png & .jpg image are allow");
                if (movieDTO.Poster.Length > _MaxAllowedPosterSize)
                    return BadRequest("Allow max size Foe Poter 1 Mb");
                using var datastreem = new MemoryStream();
                await movieDTO.Poster.CopyToAsync(datastreem);
                movie.Poster = datastreem.ToArray();
            }

            movie.StoryLIne = movieDTO.StoryLIne;
            movie.Rate=movieDTO.Rate;
            movie.Year = movieDTO.Year;
            movie.GenraId = movieDTO.GenraId;
            movie.Title = movieDTO.Title;
            
            _dbcontext.SaveChanges();
            return Ok("update");


        }

        #endregion

        #region Delete Movie 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _dbcontext.Movies.FindAsync(id);
            if (movie == null)
                return NotFound($"NoMovie with Id =  {id} ");
            _dbcontext.Remove(movie);
            _dbcontext.SaveChanges();
            return Ok();
        }
        #endregion

    }
}
