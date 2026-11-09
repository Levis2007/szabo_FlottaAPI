using Argon2Sharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using szabo_FlottaAPI.DTOs;
using szabo_FlottaAPI.Models;

namespace szabo_FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FlottaContext _context;

        public UserController(FlottaContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUser")]

        public IActionResult GetAllUser()
        {
            try
            {
                List<User> users = _context.Users.ToList();
                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound(users);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren" + ex);
            }
        }

        [HttpGet("GetUsersById{id}")]

        public IActionResult GetUsersById(int id)
        {
            try
            {
                User user = _context.Users.FirstOrDefault();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren" + ex);
            }
        }


        [HttpPost("PostUser")]

        public IActionResult PostUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás!");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba az adatbázisban" + ex);
            }
        }

        [HttpPut("PutUser")]

        public IActionResult Put(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Missing datas.");
                }
                User us = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (us == null)
                {
                    return NotFound("Missing driver.");
                }
                _context.ChangeTracker.Clear();
                _context.Users.Update(us);
                _context.SaveChanges();
                return Ok("The userdata modified");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba az adatbázisban" + ex);
            }
        }


        [HttpDelete("DeleteUser/{id}")]

        public IActionResult DeleteUser(int id)
        {
            try
            {
                User userdel = _context.Users.FirstOrDefault(u => u.Id == id);
                if (userdel != null)
                {
                    _context.Users.Remove(userdel);
                    _context.SaveChanges();
                    return Ok("Sikeres törlés!");
                }
                return NotFound("Nincs ilyen azonosítójú user!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a szerveren" + ex.Message);
            }
        }
    }
}
