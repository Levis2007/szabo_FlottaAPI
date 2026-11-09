using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using szabo_FlottaAPI.Models;

namespace szabo_FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoforController : ControllerBase
    {
        private FlottaContext _context;

        public SoforController(FlottaContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllSofor")]

        public IActionResult GetAllSoforok() 
        {
            try
            {
                List<Sofor> soforok = _context.Sofors.ToList();
                if (soforok != null)
                {
                   return Ok(soforok);
                }
                else 
                {
                   return NotFound(soforok);
                }
            }
            catch (Exception ex)
            {
               return StatusCode(500, "Hiba a szerveren" + ex);
            }
        }

        [HttpGet("GetSoforokById{id}")]

        public IActionResult GetSoforokById(int id)
        {
            try
            {
                Sofor sofor = _context.Sofors.FirstOrDefault();
                if (sofor != null)
                {
                   return Ok(sofor);
                }
                else
                {
                   return NotFound(sofor);
                }
            }
            catch (Exception ex)
            {
               return StatusCode(500, "Hiba a szerveren" + ex);
            }
        }


        [HttpPost("PostSofor")]

        public IActionResult PostSofor(Sofor sofor) 
        {
            try
            {
                _context.Sofors.Add(sofor);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás!");
            }
            catch (Exception ex)
            {

               return StatusCode(500, "Hiba az adatbázisban" + ex);
            }
        }

        [HttpPut("PutSofor")]

        public IActionResult Put(Sofor sofor) 
        {
            try
            {
                if (sofor == null)
                {
                    return BadRequest("Missing datas.");
                }
                Sofor so = _context.Sofors.FirstOrDefault(s => s.Id == sofor.Id);
                if (so == null)
                {
                    return NotFound("Missing driver.");
                }
                _context.ChangeTracker.Clear();
                _context.Sofors.Update(so);
                _context.SaveChanges();
                return Ok("The driverdata modified");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba az adatbázisban" + ex);
            }
        }


        [HttpDelete("DeleteSofor/{id}")]

        public IActionResult DeleteSoforok(int id) 
        {
            try
            {
                Sofor sofordel = _context.Sofors.FirstOrDefault(s => s.Id == id);
                if (sofordel != null)
                {
                    _context.Sofors.Remove(sofordel);
                    _context.SaveChanges();
                    return Ok("Sikeres törlés!");
                }
                return NotFound("Nincs ilyen azonosítójú sofőr!");
            }
            catch (Exception ex)
            {
               return StatusCode(500, "Hiba a szerveren" + ex.Message);
            }
        }
    }
}
