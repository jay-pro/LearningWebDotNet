using lms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassAdminController : ControllerBase
    {
        private readonly LMSContext _context;
        public ClassAdminController(LMSContext context)
        {
            _context = context;
        }

        // Chuc nang lay all cac thac mac 
        [HttpGet]
        [Route("reply")]
        public async Task<ActionResult<IEnumerable<Reply>>> GetReply()
        {
            var listReply = await _context.Reply.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh Reply học thành công",
                Data = listReply
            });
        }
        // Chuc nang lay mot thac mac
        [HttpGet]
        [Route("reply/{IDReply}")]
        public async Task<ActionResult<Reply>> GetReply(string IDReply)
        {
            var reply = await _context.Reply.FindAsync(IDReply);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy Reply thành công",
                Data = reply
            });
        }
        // Chức năng giải đáp thắc mắc

        [HttpPost]
        [Route("create-reply")]

        public async Task<ActionResult<Reply>> CreateReply([FromBody] Reply reply, [FromQuery] string IDComment)
        {
            if (IDComment == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Reply phải gắn với một comment nhất định",
                    Data = ""
                });
            }
            var replyTemp = new Reply
            {
                Content = reply.Content,
                CretatedAt = DateTime.Now,
                IDUser = reply.IDUser,
                IDComment = IDComment
            };
            _context.Reply.Add(replyTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReplyExists(replyTemp.IDReply))
                {
                    return Conflict(new
                    {
                        StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = ""
                    });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get Reply", new { IDReply = replyTemp.IDReply }, new
            {
                StatusCode = 201,
                Message = "Tạo comment thành công",
                Data = replyTemp
            });
        }

        // Chuc nang cap nhat mot reply
        [HttpPut]
        [Route("reply/{IDReply}")]
        public async Task<ActionResult<Reply>> PutReply(string IDReply, [FromBody] Reply reply)
        {
            var replyTemp = await _context.Reply.FindAsync(IDReply);
            if (replyTemp == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy reply trong hệ thống",
                    Data = ""
                });
            }
            replyTemp.Content = reply.Content;
            _context.Entry(replyTemp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(IDReply))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy reply trong hệ thống",
                        Data = reply
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 204,
                Message = "Chỉnh sửa reply thành công",
                Data = reply
            });
        }
        // Chuc nang xoa mot reply
        [HttpDelete]
        [Route("reply/{IDReply}")]
        public async Task<IActionResult> DeleteReply(string IDReply)
        {
            var reply = await _context.Reply.FindAsync(IDReply);
            if (reply == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy reply trong hệ thống",
                    Data = reply
                });
            }

            _context.Reply.Remove(reply);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Xoa reply thành công",
                Data = reply
            });
        }



        // Chuc nang lay all cac thac mac 
        [HttpGet]
        [Route("rating")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRating()
        {
            var listRating = await _context.Rating.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh Rating thành công",
                Data = listRating
            });
        }
        // Chuc nang lay mot thac mac
        [HttpGet]
        [Route("rating/{Id}")]
        public async Task<ActionResult<Rating>> GetRating(string Id)
        {
            var rating = await _context.Rating.FindAsync(Id);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy rating thành công",
                Data = rating
            });
        }


        // Chức năng đánh giá học viên 
        [HttpPost]
        [Route("create-rating")]
        public async Task<ActionResult<Rating>> CreateRating([FromBody] Rating rating)
        {
            var ratingTemp = new Rating
            {
                Content = rating.Content,
                rating = rating.rating,
                IDUser = rating.IDUser
            };
            _context.Rating.Add(ratingTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RatingExists(rating.Id))
                {
                    return Conflict(new
                    {
                        StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = ""
                    });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get Rating", new { Id = rating.Id }, new
            {
                StatusCode = 201,
                Message = "Tạo đánh giá thành công",
                Data = ratingTemp
            });

            //return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
        }


        // Chuc nang cap nhat mot rating
        [HttpPut]
        [Route("rating/{Id}")]
        public async Task<ActionResult<Rating>> PutRating(string Id, [FromBody] Rating rating)
        {
            var ratingTemp = await _context.Rating.FindAsync(Id);
            if (ratingTemp == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy rating trong hệ thống",
                    Data = ""
                });
            }
            ratingTemp.rating = rating.rating;
            _context.Entry(ratingTemp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(Id))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy rating trong hệ thống",
                        Data = ratingTemp
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 204,
                Message = "Chỉnh sửa rating thành công",
                Data = ratingTemp
            });
        }
        // Chuc nang xoa mot rating
        [HttpDelete]
        [Route("rating/{Id}")]
        public async Task<IActionResult> DeleteRating(string Id)
        {
            var rating = await _context.Reply.FindAsync(Id);
            if (rating == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy rating trong hệ thống",
                    Data = rating
                });
            }

            _context.Reply.Remove(rating);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Xoa rating thành công",
                Data = rating
            });
        }


        // Chức năng gửi thông báo đến học viên

        [HttpGet]
        [Route("notify")]
        public async Task<ActionResult<IEnumerable<Notify>>> GetNotify()
        {
            var listNotify = await _context.Notify.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh Notify thành công",
                Data = listNotify
            });
        }
        // Chuc nang lay mot thong bao
        [HttpGet]
        [Route("notify/{IDNotify}")]
        public async Task<ActionResult<Notify>> GetNotify(string IDNotify)
        {
            var notify = await _context.Notify.FindAsync(IDNotify);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy rating thành công",
                Data = notify
            });
        }
        // Chức năng gửi thông báo đến học viên
        [HttpPost]
        [Route("create-notify")]
        public async Task<ActionResult<Notify>> CreateNotify([FromBody] Notify notify)
        {
            var tempNotify = new Notify
            {
                Title = notify.Title,
                Content = notify.Content,
                CretatedAt = DateTime.Now,
                IDUser = notify.IDUser
               
            };
            _context.Notify.Add(tempNotify);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!NotifyExists(tempNotify.IDNotify))
                {
                    return Conflict(new
                    {
                        StatusCode = 409,
                        Message = "Đã xảy ra xung đột khi thêm vào cơ sỏ dữ liệu",
                        Data = ""
                    });
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Get Notify", new { IDNotify = tempNotify.IDNotify }, new
            {
                StatusCode = 201,
                Message = "Tạo đánh giá thành công",
                Data = tempNotify
            });

            //return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
        }


        // Chuc nang cap nhat mot notify
        [HttpPut]
        [Route("notify/{IDNotify}")]
        public async Task<ActionResult<Notify>> PutNotify(string IDNotify, [FromBody] Notify notify)
        {
            var tempNotify = await _context.Notify.FindAsync(IDNotify);
            if (tempNotify == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy notify trong hệ thống",
                    Data = ""
                });
            }
            tempNotify.Title = notify.Title;
            tempNotify.Content = notify.Content;
            _context.Entry(tempNotify).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotifyExists(IDNotify))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy notify trong hệ thống",
                        Data = tempNotify
                    });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new
            {
                StatusCode = 204,
                Message = "Chỉnh sửa notify thành công",
                Data = tempNotify
            });
        }
        // Chuc nang xoa mot notify
        [HttpDelete]
        [Route("notify/{IDNotify}")]
        public async Task<IActionResult> DeleteNotify(string IDNotify)
        {
            var notify = await _context.Notify.FindAsync(IDNotify);
            if (notify == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy notify trong hệ thống",
                    Data = notify
                });
            }

            _context.Notify.Remove(notify);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Xoa notify thành công",
                Data = notify
            });
        }

        private bool ReplyExists(string IDReply)
        {
            return _context.Reply.Any(e => e.IDReply == IDReply);
        }
        private bool RatingExists(string id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
        private bool NotifyExists(string IDNotify)
        {
            return _context.Notify.Any(e => e.IDNotify == IDNotify);
        }
    }
}
