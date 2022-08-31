using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using postgis.Infrastructures.Context;
using postgis.Models;
using System.Threading.Tasks;

namespace postgis.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/controller")]
    public class CityController : Controller
    {
        private readonly IApplicationDbContext _context;

        public CityController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Cities")]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                var result = await _context.Cities.ToListAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("City/Id")]
        public async Task<IActionResult> GetCity(int id)
        {
            try
            {
                var result = await _context.Cities.FirstOrDefaultAsync(x => x.ID == id);
                if (result is null)
                    return NotFound();

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            try
            {
                if (city == null || city.ID > 0)
                    return BadRequest();

                _context.Cities.Add(city);
                await _context.SaveChangeAsync();

                return Ok(city);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCity([FromBody] City city)
        {
            try
            {
                if (city == null || city.ID < 1)
                    return BadRequest();

                var existingCity = await _context.Cities.FirstOrDefaultAsync(x => x.ID == city.ID);
                if (existingCity is null)
                    return NotFound();
                existingCity.Name = city.Name;
                _context.Cities.Update(existingCity);
                await _context.SaveChangeAsync();

                return Ok(existingCity);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/id")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();

                var existingCity = await _context.Cities.FirstOrDefaultAsync(x => x.ID == id);
                if (existingCity is null)
                    return NotFound();

                _context.Cities.Remove(existingCity);
                await _context.SaveChangeAsync();

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
