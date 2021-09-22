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
    public class MentorController : ControllerBase
    {
        private readonly LMSContext _context;
        public MentorController(LMSContext context)
        {
            _context = context;
        }

        // Chuc nang lay all cac thac mac 
        [HttpGet]
        [Route("attend")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance()
        {
            var listAttend = await _context.Attendance.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh attend học thành công",
                Data = listAttend
            });
        }
        // Chuc nang lay mot thac mac
        [HttpGet]
        [Route("attend/{IDAttendance}")]
        public async Task<ActionResult<Attendance>> GetAttendance(string IDAttendance)
        {
            var attend = await _context.Attendance.FindAsync(IDAttendance);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy attend thành công",
                Data = attend
            });
        }

        // Chức năng điểm danh học viên
        [HttpPost]
        [Route("create-attend")]
        public async Task<ActionResult<Attendance>> CreateAttendance([FromBody] Attendance attendance)
        {
            var attendTemp = new Attendance
            {
                isChecked = attendance.isChecked,
                Total     = attendance.Total,
                IDUser    = attendance.IDUser
            };
            _context.Attendance.Add(attendTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!AttendanceExists(attendTemp.IDAttendance))
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
            return CreatedAtAction("Get Attendance", new { IDAttendance = attendTemp.IDAttendance }, new
            {
                StatusCode = 201,
                Message = "Tạo điểm danh thành công",
                Data = attendTemp
            });
        }
        // Chuc nang cap nhat mot reply
        [HttpPut]
        [Route("attend/{IDAttendance}")]
        public async Task<ActionResult<Attendance>> PutReply(string IDAttendance, [FromBody] Attendance attendance)
        {
            var attendTemp = await _context.Attendance.FindAsync(IDAttendance);
            if (attendTemp == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy reply trong hệ thống",
                    Data = ""
                });
            }
            attendTemp.isChecked = attendance.isChecked;
            attendTemp.Total = attendance.Total;
            _context.Entry(attendTemp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(IDAttendance))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy attend trong hệ thống",
                        Data = attendTemp
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
                Message = "Chỉnh sửa attend thành công",
                Data = attendTemp
            });
        }
        // Chuc nang xoa mot attend
        [HttpDelete]
        [Route("attend/{IDAttendance}")]
        public async Task<IActionResult> DeleteAttendance(string IDAttendance)
        {
            var attend = await _context.Attendance.FindAsync(IDAttendance);
            if (attend == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy attend trong hệ thống",
                    Data = attend
                });
            }

            _context.Attendance.Remove(attend);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Xoa attend thành công",
                Data = attend
            });
        }




        // ----------------------------------///

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
            if(IDComment == null)
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
            return CreatedAtAction("Get Reply", new {  IDReply = replyTemp.IDReply  }, new
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
        // Chuc nang lay all diem
        [HttpGet]
        [Route("quizzscore")]
        public async Task<ActionResult<IEnumerable<QuizzScore>>> GetQuizzScore()
        {
            var listQS = await _context.QuizzScore.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy danh sach QuizzScore học thành công",
                Data = listQS
            });
        }
        // Chuc nang lay mot diem
        [HttpGet]
        [Route("quizzscore/{IDQuizzScore}")]
        public async Task<ActionResult<QuizzScore>> GetQuizzScore(string IDQuizzScore)
        {
            var qscore = await _context.QuizzScore.FindAsync(IDQuizzScore);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lấy QS thành công",
                Data = qscore
            });
        }
        // Chức năng nhập điểm 
        [HttpPost]
        [Route("create-quizzscore")]
        public async Task<ActionResult<QuizzScore>> CreateQuizzScore([FromBody] QuizzScore quizzScore, [FromQuery] string IDQuizz)
        {
            if (IDQuizz == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "QuizzScore phải gắn với một IDQuizz nhất định",
                    Data = ""
                });
            }
            var qsTemp = new QuizzScore
            {
                Score   = quizzScore.Score,
                IDUser  = quizzScore.IDUser,
                //Quizz   = quizzScore.Quizz,
                IDQuizz = IDQuizz

            };
            _context.QuizzScore.Add(qsTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!QuizzScoreExists(qsTemp.IDQuizzScore))
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
            return CreatedAtAction("Get QuizzScore", new { IDQuizzScore = qsTemp.IDQuizzScore }, new
            {
                StatusCode = 201,
                Message = "Tạo nhập điểm thành công",
                Data = qsTemp
            });
        }
        // Chuc nang cap nhat mot reply
        [HttpPut]
        [Route("quizzscore/{IDQuizzScore}")]
        public async Task<ActionResult<QuizzScore>> PutQuizzScore(string IDQuizzScore, [FromBody] QuizzScore quizzscore)
        {
            var qsTemp = await _context.QuizzScore.FindAsync(IDQuizzScore);
            if (qsTemp == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy qs trong hệ thống",
                    Data = ""
                });
            }
            qsTemp.Score = quizzscore.Score;
            //qsTemp.Quizz = quizzscore.Quizz;
            _context.Entry(qsTemp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizzScoreExists(IDQuizzScore))
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Không tìm thấy qs trong hệ thống",
                        Data = qsTemp
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
                Message = "Chỉnh sửa qs thành công",
                Data = qsTemp
            });
        }
        // Chuc nang xoa mot quizzscore
        [HttpDelete]
        [Route("quizzscore/{IDQuizzScore}")]
        public async Task<IActionResult> DeleteQuizzScore(string IDQuizzScore)
        {
            var qscore = await _context.QuizzScore.FindAsync(IDQuizzScore);
            if (qscore == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy quizzscore trong hệ thống",
                    Data = qscore
                });
            }

            _context.QuizzScore.Remove(qscore);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Xoa quizzscore thành công",
                Data = qscore
            });
        }

        private bool AttendanceExists(string IDAttendance)
        {
            return _context.Attendance.Any(e => e.IDAttendance == IDAttendance);
        }
        private bool ReplyExists(string IDReply)
        {
            return _context.Reply.Any(e => e.IDReply == IDReply);
        }
        private bool CommentExists(string IDComment)
        {
            return _context.Comment.Any(e => e.IDComment == IDComment);
        }
        private bool QuizzScoreExists(string IDQuizzScore)
        {
            return _context.QuizzScore.Any(e => e.IDQuizzScore == IDQuizzScore);
        }

    }
}
