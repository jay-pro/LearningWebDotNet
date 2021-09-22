using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class LMSContext : DbContext
    {        public LMSContext(DbContextOptions<LMSContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<ClassAdmin> ClassAdmin { get; set;}
        public DbSet<Course> Course { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Mentor> Mentor { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<SystemAdmin> SystemAdmin { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet <Class> Class { get; set; }
        public DbSet <Enrollment> Enrollment { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<ScoreBroad> ScoreBroad { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet <Quizz> Quizz { get; set; }
        public DbSet <QuizzDetail> QuizzDetail { get; set; }
        public DbSet <CourseFeedback> CourseFeedback { get; set; }
        public DbSet <QuizzScore> QuizzScore { get; set; }
        public DbSet <Reply> Reply { get; set; }
        public DbSet <Notify> Notify { get; set; }
        public DbSet <StudentFeedback> StudentFeedbacks { get; set; }
        public DbSet <Assignment> Assignment { get; set; }
        public DbSet <AssignmentSubmission> AssignmentSubmission { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
    }
}
