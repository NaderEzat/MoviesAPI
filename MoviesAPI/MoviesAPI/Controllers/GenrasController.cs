using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOS;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenrasController : ControllerBase
    {
        private readonly AppDBcontext _context;

        public GenrasController(AppDBcontext context)
        {
            _context = context;
        }

        #region Get All Genre
        [HttpGet]
        public async Task<IActionResult> GetAllGenra()
        {
            var genars = await _context.Genras.OrderBy(g=>g.Name).ToListAsync();
            return Ok(genars);
        }

        #endregion


        #region Create new Genre
        [HttpPost]
        public async Task<IActionResult> AddGenra(CreateGrnraDto dto)
        {
            var ganrea = new Genra { Name = dto.Name };
            await _context.Genras.AddAsync(ganrea);
            _context.SaveChanges();
            return Ok(ganrea);
        }
        #endregion


        #region Update Genre 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenra(int id, [FromBody] CreateGrnraDto dto)
        {
            var genra = await _context.Genras.FirstOrDefaultAsync(g => g.Id == id);
            if(genra == null)
            {
                return NotFound($"No Genra found with Id :{id}");
            }
            genra.Name = dto.Name;
            _context.SaveChanges();
            return (Ok(genra));
        }
        #endregion

        #region Delete Genre 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenra(int id)
        {
            var genra = await _context.Genras.FirstOrDefaultAsync(g => g.Id == id);
            if (genra == null)
            {
                return NotFound($"No Genra found with Id :{id}");
            }
            _context.Remove(genra);
            _context.SaveChanges();

            return Ok();
        }
        #endregion
    }
}
