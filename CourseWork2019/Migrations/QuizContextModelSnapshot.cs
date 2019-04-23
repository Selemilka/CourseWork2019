﻿// <auto-generated />
using System;
using CourseWork2019.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourseWork2019.Migrations
{
    [DbContext(typeof(QuizContext))]
    partial class QuizContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseWork2019.Models.Answer", b =>
                {
                    b.Property<int>("AnswerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerText");

                    b.Property<int>("QuestionID");

                    b.HasKey("AnswerID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("CourseWork2019.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("QuestionName")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("QuestionText");

                    b.Property<int>("RubricID");

                    b.HasKey("QuestionID");

                    b.HasIndex("RubricID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("CourseWork2019.Models.Quiz", b =>
                {
                    b.Property<int>("QuizID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("QuizName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("QuizID");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("CourseWork2019.Models.QuizQuestion", b =>
                {
                    b.Property<int>("QuizID");

                    b.Property<int>("QuestionID");

                    b.Property<int>("QuizQuestionID");

                    b.HasKey("QuizID", "QuestionID");

                    b.HasIndex("QuestionID");

                    b.ToTable("QuizQuestions");
                });

            modelBuilder.Entity("CourseWork2019.Models.Rubric", b =>
                {
                    b.Property<int>("RubricID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RubricName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("RubricID");

                    b.ToTable("Rubrics");
                });

            modelBuilder.Entity("CourseWork2019.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("FirstMidName")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseWork2019.Models.UserQuiz", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("QuizID");

                    b.Property<int>("UserQuizID");

                    b.HasKey("UserID", "QuizID");

                    b.HasIndex("QuizID");

                    b.ToTable("UserQuizzes");
                });

            modelBuilder.Entity("CourseWork2019.Models.Answer", b =>
                {
                    b.HasOne("CourseWork2019.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork2019.Models.Question", b =>
                {
                    b.HasOne("CourseWork2019.Models.Rubric", "Rubric")
                        .WithMany("Questions")
                        .HasForeignKey("RubricID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork2019.Models.QuizQuestion", b =>
                {
                    b.HasOne("CourseWork2019.Models.Question", "Question")
                        .WithMany("QuizQuestions")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseWork2019.Models.Quiz", "Quiz")
                        .WithMany("QuizQuestions")
                        .HasForeignKey("QuizID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork2019.Models.UserQuiz", b =>
                {
                    b.HasOne("CourseWork2019.Models.Quiz", "Quiz")
                        .WithMany("UserQuizzes")
                        .HasForeignKey("QuizID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseWork2019.Models.User", "User")
                        .WithMany("UserQuizzes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
