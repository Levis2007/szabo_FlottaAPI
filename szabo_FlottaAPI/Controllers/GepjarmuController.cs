using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using szabo_FlottaAPI.Models;

namespace szabo_FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GepjarmuController : ControllerBase
    {
        private FlottaContext _context;

        public GepjarmuController(FlottaContext context) 
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult Get() 
        {
            try
            {
                return Ok(_context.Gepjarmus.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba: " + ex.Message);
            }  
        }

        [HttpGet("GetByRendszam/{rendszam}")]
        public IActionResult GetByRendszam(string rendszam) 
        {
            try
            {
                return Ok(_context.Gepjarmus.FirstOrDefault(r => r.Rendszam == rendszam));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba: " + ex.Message);
            }
        }

        [HttpPost("UjGepjarmu")]
        public IActionResult UjGepjarmu(Gepjarmu ujGepjarmu) 
        {

            try
            {
                if (ujGepjarmu == null)
                {
                    return BadRequest("Hiányzó adatok.");
                }
               
                    _context.Gepjarmus.Add(ujGepjarmu);
                    _context.SaveChanges();
                    return Ok("Sikeres mentés!");
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba: " + ex.Message);
            }
        }

        [HttpGet("GetKiVezetteAJarmuvetById")]

        public IActionResult GetKiVezetteAJarmuvetById(int id) 
        {
            try
            {
                if (_context.Gepjarmus.FirstOrDefault(g => g.Id == id) == null)
                {
                    return NotFound("Hiányzó gépjármű!");
                }
                List<Sofor> result = _context.Kikuldottjarmus.Include(k => k.SoforNavigation)
                                                             .Include(k => k.Gepjarmu)
                                                             .Where(k => k.GepjarmuId == id)
                                                             .Select(k => k.SoforNavigation)
                                                             .ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba: " + ex.Message);
            }
        }

        [HttpDelete("DeleteGepjarmuById/{id}")]
        public IActionResult DeleteGepjarmuById(int id) 
        {
            try
            {
                if (_context.Gepjarmus.FirstOrDefault(g => g.Id == id) == null)
                {
                    return NotFound("Hiányzó gépjármű!");
                }
                _context.ChangeTracker.Clear();
                _context.Gepjarmus.Remove(new Gepjarmu { Id = id });
                _context.SaveChanges();
                return Ok("Sikeres törlés!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba: " + ex.Message);
            }
        }
    }
}
