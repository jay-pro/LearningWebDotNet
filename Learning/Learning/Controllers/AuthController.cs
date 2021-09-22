using lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly LMSContext _context; 
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly MailSettings _mailSettings;

        public AuthController(LMSContext context, IConfiguration config, UserManager<User> userManager, IOptions<MailSettings> mailSettings,
            SignInManager<User> signInManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mailSettings = mailSettings.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LogInModel login)
        {
            var user = await AuthenticateUser(login.UserName, login.Password);
            if (user!=null)
            {
                if (user.LockoutEnabled == true)
                {
                    if (user.LockoutEnd != null)
                        if (user.LockoutEnd > DateTime.Now)
                            return Unauthorized(new
                            {
                                Message = "Tài khoản của bạn đang bị khóa"
                            }); ;
                }
                if (user.Status == "waitingforapproval" || user.Status == "denied")
                {
                    return Unauthorized(new
                    {
                        Message = "Tài khoản của bạn chưa được duyệt"
                    });
                }
                var tokenString = await GenerateJSONWebToken(user);
                    return Ok(new { token = tokenString });
            }

            return Unauthorized(new { 
                Message = "Sai tài khoản hoặc mật khẩu"
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<User>> Register(LogInModel login)
        {
            string link;
            User user = new User()
            {
                UserName = login.UserName,
                Email = login.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user, login.Password);
                if(result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailSender es = new EmailSender(_mailSettings);
                    link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
                    await es.SendConfirmEmailAsync(user.Email, link, "Confirm email","[LMS] Confirm email");
                } else
                {
                    return Ok(
                        new
                        {
                            Message = "Lỗi",
                            Data = result.Errors
                        });
                }
            }
            catch (Exception)
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

            return Ok( new { 
                Message = "Đăng ký thành công",
                Data = link
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register-student")]
        public async Task<ActionResult<User>> RegisterStudent(LogInModel login)
        {
            string link;
            try
            {
                User user = new User()
                {
                    UserName = login.UserName,
                    Email = login.Email
                };
                var result = await _userManager.CreateAsync(user, login.Password);
                Student student = new Student()
                {
                    User = user,
                    IDUser = user.Id
                };
                user.Status = "approval";
                _context.Student.Add(student);
                if (result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailSender es = new EmailSender(_mailSettings);
                    link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
                    await es.SendConfirmEmailAsync(user.Email, link, "Confirm email", "[LMS] Confirm email");
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(
                        new
                        {
                            Message = "Lỗi",
                            Data = result.Errors
                        });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi",
                    Data = e.Data
                });
            }

            return Ok(new
            {
                Message = "Đăng ký thành công",
                Data = link
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register-system-admin")]
        public async Task<ActionResult<User>> RegisterSysAdmin(LogInModel login)
        {
            string link;
            try
            {
                User user = new User()
                {
                    UserName = login.UserName,
                    Email = login.Email
                };
                var result = await _userManager.CreateAsync(user, login.Password);
                SystemAdmin teacher = new SystemAdmin()
                {
                    User = user,
                    IDUser = user.Id
                };


                _context.SystemAdmin.Add(teacher);
                if (result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailSender es = new EmailSender(_mailSettings);
                    link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
                    await es.SendConfirmEmailAsync(user.Email, link, "Confirm email", "[LMS] Confirm email");
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(
                        new
                        {
                            Message = "Lỗi",
                            Data = result.Errors
                        });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi",
                    Data = e.Data
                });
            }

            return Ok(new
            {
                Message = "Đăng ký thành công",
                //Data = link
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register-teacher")]
        public async Task<ActionResult<User>> RegisterTeacher(LogInModel login)
        {
            string link;
            try
            {
                User user = new User()
                {
                    UserName = login.UserName,
                    Email = login.Email
                };
                var result = await _userManager.CreateAsync(user, login.Password);
                Teacher teacher = new Teacher()
                {
                    User = user,
                    IDUser = user.Id
                };
                user.Status = "waitingforapproval";
                _context.Teacher.Add(teacher);
                if (result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailSender es = new EmailSender(_mailSettings);
                    link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
                    await es.SendConfirmEmailAsync(user.Email, link, "Confirm email", "[LMS] Confirm email");
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(
                        new
                        {
                            Message = "Lỗi",
                            Data = result.Errors
                        });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi",
                    Data = e.Data
                });
            }

            return Ok(new
            {
                Message = "Đăng ký thành công",
                Data = link
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register-instructor")]
        public async Task<ActionResult<User>> RegisterInstructor(LogInModel login)
        {
            string link;
            try
            {
                User user = new User()
                {
                    UserName = login.UserName,
                    Email = login.Email
                };
                var result = await _userManager.CreateAsync(user, login.Password);
                Instructor instructor = new Instructor()
                {
                    User = user,
                    IDUser = user.Id
                };
                user.Status = "waitingforapproval";
                _context.Instructor.Add(instructor);
                if (result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailSender es = new EmailSender(_mailSettings);
                    link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
                    await es.SendConfirmEmailAsync(user.Email, link, "Confirm email", "[LMS] Confirm email");
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(
                        new
                        {
                            Message = "Lỗi",
                            Data = result.Errors
                        });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi",
                    Data = e.Data
                });
            }

            return Ok(new
            {
                Message = "Đăng ký thành công",
                Data = link
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register-mentor")]
        public async Task<ActionResult<User>> RegisterMentor(LogInModel login)
        {
            string link;
            try
            {
                User user = new User()
                {
                    UserName = login.UserName,
                    Email = login.Email
                };
                var result = await _userManager.CreateAsync(user, login.Password);
                Mentor mentor = new Mentor()
                {
                    User = user,
                    IDUser = user.Id
                };
                user.Status = "waitingforapproval";
                _context.Mentor.Add(mentor);
                if (result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailSender es = new EmailSender(_mailSettings);
                    link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
                    await es.SendConfirmEmailAsync(user.Email, link, "Confirm email", "[LMS] Confirm email");
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(
                        new
                        {
                            Message = "Lỗi",
                            Data = result.Errors
                        });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi",
                    Data = e.Message
                });
            }

            return Ok(new
            {
                Message = "Đăng ký thành công",
                Data = link
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register-class-admin")]
        public async Task<ActionResult<User>> RegisterClassAdmin(LogInModel login)
        {
            string link;
            try
            {
                User user = new User()
                {
                    UserName = login.UserName,
                    Email = login.Email
                };
                var result = await _userManager.CreateAsync(user, login.Password);
                ClassAdmin classAdmin = new ClassAdmin()
                {
                    User = user,
                    IDUser = user.Id
                };
                user.Status = "waitingforapproval";
                _context.ClassAdmin.Add(classAdmin);
                if (result.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailSender es = new EmailSender(_mailSettings);
                    link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
                    await es.SendConfirmEmailAsync(user.Email, link, "Confirm email", "[LMS] Confirm email");
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(
                        new
                        {
                            Message = "Lỗi",
                            Data = result.Errors
                        });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "Lỗi",
                    Data = e.Message
                });
            }

            return Ok(new
            {
                Message = "Đăng ký thành công",
                Data = link
            });
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("confirm-email", Name = "ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string token, string userId)
        {
            try
            {
                User user = await _context.User.FindAsync(userId);
                user.EmailConfirmed = true;
                await _userManager.ConfirmEmailAsync(user, token);
                return Redirect("http://localhost:3000/login");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forget-password")]
        public async Task<ActionResult<User>> ForgetPassword(string email)
        {
            User u = await _userManager.FindByEmailAsync(email);
            if(u != null)
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(u);
                EmailSender es = new EmailSender(_mailSettings);
                //string link = Url.Link("ResetPassword", new { userId = u.Id, token = token });
                string encoded = HttpUtility.UrlEncode(token);
                string link = "http://localhost:3000/reset-password/" + u.Id + "/" + encoded;
                await es.SendConfirmEmailAsync(u.Email, link, "Reset password", "[LMS] Reset password");
            }

            return Ok( new
            {
                Message = "Đã gửi email khôi phục",
                Data = ""
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password", Name = "ResetPassword")]
        public async Task<ActionResult<User>> ResetPassword(string userId, string token, string newPassword)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(userId);
                string decoded = HttpUtility.UrlDecode(token);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, decoded, newPassword);
                if(result.Succeeded)
                    return Ok(new
                    {
                        Message = "Đã đặt lại mật khẩu",
                        Data = ""
                    });
                else
                {
                    return BadRequest(new
                    {
                        Message = "Thất bại",
                        Data = result.Errors.First().Description
                    });
                }
            } catch
            {
                return BadRequest(new
                {
                    Message = "Thất bại",
                    Data = ""
                });
            }
        }
        private async Task<string> GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string role = await getRole(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role,role),
                new Claim("UserName", user.UserName),
                new Claim("Uid", user.Id),
                new Claim("Role", role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> AuthenticateUser(string username, string password)
        {
            User user = null;
            user = await _context.User.Where(u => u.UserName == username && u.EmailConfirmed == true)
                .FirstOrDefaultAsync();
            if (user == null)
                return null;           
            var result = await _userManager.CheckPasswordAsync(user, password);
            if(result)
                return user;
            return null;
        }
        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
        private async Task SendConfirmEmail(User user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            EmailSender es = new EmailSender(_mailSettings);
            string link = Url.Link("ConfirmEmail", new { userId = user.Id, token = token });
            await es.SendConfirmEmailAsync(user.Email, link, "Confirm email", "[LMS] Confirm email");
        }

        private async Task<string> getRole(User user)
        {
            if (await _context.Teacher.AnyAsync(u => u.IDUser == user.Id))
                return "Teacher";
            if (await _context.Instructor.AnyAsync(u => u.IDUser == user.Id))
                return "Instructor";
            if (await _context.SystemAdmin.AnyAsync(u => u.IDUser == user.Id))
                return "SystemAdmin";
            if (await _context.Mentor.AnyAsync(u => u.IDUser == user.Id))
                return "Mentor";
            return "Student";
        }
    }
}
