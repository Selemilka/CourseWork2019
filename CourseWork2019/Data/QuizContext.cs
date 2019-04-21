using CourseWork2019.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork2019.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<CorrectAnswer>().ToTable("Enrollment");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<Quiz>().ToTable("Quizzes");
            modelBuilder.Entity<QuizQuestion>().ToTable("QuizQuestions");
            modelBuilder.Entity<Rubric>().ToTable("Rubrics");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserQuiz>().ToTable("UserQuizzes");

            modelBuilder.Entity<QuizQuestion>()
                .HasKey(c => new { c.QuizID, c.QuestionID });
            modelBuilder.Entity<UserQuiz>()
                .HasKey(c => new { c.UserID, c.QuizID });

            modelBuilder.Entity<QuizQuestion>()
            .HasOne(sc => sc.Quiz)
            .WithMany(s => s.QuizQuestions)
            .HasForeignKey(sc => sc.QuizID);

            modelBuilder.Entity<QuizQuestion>()
                .HasOne(sc => sc.Question)
                .WithMany(c => c.QuizQuestions)
                .HasForeignKey(sc => sc.QuestionID);

        }

    }
}
