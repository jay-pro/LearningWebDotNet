using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(LMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return Ok(new { Message = "OK", Data = await _context.User.ToListAsync()});
        }

        [HttpGet("info/{id}")]
        public async Task<ActionResult<User>> GetUserInfo(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "Không tìm thấy thông tin" });
            }
            var uInfo = new
            {
                user.Avatar,
                user.Email,
                user.UserName,
                user.Fullname,
                user.PhoneNumber,
                user.DateOfBirth
            };
            return Ok(new { Message = "", Data = uInfo });
        }
        [HttpPut("info/update/{id}")]
        public async Task<ActionResult<User>> UpdateUserInfo(string id, User userInfo)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "Không tìm thấy thông tin" });
            }
            user.Avatar = userInfo.Avatar;
            user.Fullname = userInfo.Fullname;
            user.PhoneNumber = userInfo.PhoneNumber;
            user.DateOfBirth = userInfo.DateOfBirth;
            user.Address = userInfo.Address;
            try
            {
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Cập nhật thành công" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Lỗi", Data = e.Data });
            }
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "Không tìm thấy user này"});
            }

            return Ok(new { Message = "", Data = user });
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, new { Data = user });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
