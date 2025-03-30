﻿// <auto-generated />
using System;
using Edu_System_BackEnd.Edu_System_BackEnd.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Edu_System_BackEnd.Migrations
{
    [DbContext(typeof(Edu_System_BackEndDbContext))]
    [Migration("20250330193006_UpdateClassSupervision")]
    partial class UpdateClassSupervision
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Attendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_present");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("TEXT")
                        .HasColumnName("lesson_id");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT")
                        .HasColumnName("student_id");

                    b.HasKey("Id")
                        .HasName("pk_attendances");

                    b.HasIndex("LessonId")
                        .HasDatabaseName("ix_attendances_lesson_id");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("ix_attendances_student_id");

                    b.ToTable("attendances", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.ClassSupervision", b =>
                {
                    b.Property<Guid>("TeacherId")
                        .HasColumnType("TEXT")
                        .HasColumnName("teacher_id");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("TEXT")
                        .HasColumnName("class_id");

                    b.HasKey("TeacherId", "ClassId")
                        .HasName("pk_class_supervisions");

                    b.HasIndex("ClassId")
                        .HasDatabaseName("ix_class_supervisions_class_id");

                    b.ToTable("class_supervisions", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Homework", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("AssignedData")
                        .HasColumnType("TEXT")
                        .HasColumnName("assigned_data");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("due_date");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("TEXT")
                        .HasColumnName("lesson_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_homeworks");

                    b.HasIndex("LessonId")
                        .HasDatabaseName("ix_homeworks_lesson_id");

                    b.ToTable("homeworks", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.HomeworkSubmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("file_path");

                    b.Property<Guid>("HomeworkId")
                        .HasColumnType("TEXT")
                        .HasColumnName("homework_id");

                    b.Property<Guid>("HomeworkMarkId")
                        .HasColumnType("TEXT")
                        .HasColumnName("homework_mark_id");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("status");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT")
                        .HasColumnName("student_id");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("submitted_at");

                    b.HasKey("Id")
                        .HasName("pk_homework_submissions");

                    b.HasIndex("HomeworkId")
                        .HasDatabaseName("ix_homework_submissions_homework_id");

                    b.HasIndex("HomeworkMarkId")
                        .IsUnique()
                        .HasDatabaseName("ix_homework_submissions_homework_mark_id");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("ix_homework_submissions_student_id");

                    b.ToTable("homework_submissions", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Classroom")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("classroom");

                    b.Property<DateTime>("LessonDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("lesson_date");

                    b.Property<Guid>("SchoolClassId")
                        .HasColumnType("TEXT")
                        .HasColumnName("school_class_id");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("TEXT")
                        .HasColumnName("subject_id");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("TEXT")
                        .HasColumnName("teacher_id");

                    b.HasKey("Id")
                        .HasName("pk_lessons");

                    b.HasIndex("SchoolClassId")
                        .HasDatabaseName("ix_lessons_school_class_id");

                    b.HasIndex("SubjectId")
                        .HasDatabaseName("ix_lessons_subject_id");

                    b.HasIndex("TeacherId")
                        .HasDatabaseName("ix_lessons_teacher_id");

                    b.ToTable("lessons", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Mark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT")
                        .HasColumnName("discriminator");

                    b.Property<int>("Grade")
                        .HasColumnType("INTEGER")
                        .HasColumnName("grade");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT")
                        .HasColumnName("student_id");

                    b.HasKey("Id")
                        .HasName("pk_marks");

                    b.ToTable("marks", (string)null);

                    b.HasDiscriminator().HasValue("Mark");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.SchoolClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("TEXT")
                        .HasColumnName("teacher_id");

                    b.HasKey("Id")
                        .HasName("pk_school_classes");

                    b.HasIndex("TeacherId")
                        .HasDatabaseName("ix_school_classes_teacher_id");

                    b.ToTable("school_classes", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.StudentParent", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT")
                        .HasColumnName("student_id");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("TEXT")
                        .HasColumnName("parent_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.HasKey("StudentId", "ParentId")
                        .HasName("pk_student_parents");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_student_parents_parent_id");

                    b.ToTable("student_parents", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_subjects");

                    b.ToTable("subjects", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("middle_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password_hash");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT")
                        .HasColumnName("role_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_roles_role_id");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.HomeworkMark", b =>
                {
                    b.HasBaseType("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Mark");

                    b.Property<Guid>("HomeworkSubmissionId")
                        .HasColumnType("TEXT")
                        .HasColumnName("homework_submission_id");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("ix_marks_student_id");

                    b.HasDiscriminator().HasValue("HomeworkMark");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.LessonMark", b =>
                {
                    b.HasBaseType("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Mark");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("TEXT")
                        .HasColumnName("lesson_id");

                    b.HasIndex("LessonId")
                        .HasDatabaseName("ix_marks_lesson_id");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("ix_marks_student_id");

                    b.HasDiscriminator().HasValue("LessonMark");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Parent", b =>
                {
                    b.HasBaseType("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User");

                    b.ToTable("parents", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", b =>
                {
                    b.HasBaseType("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User");

                    b.Property<Guid>("SchoolClassId")
                        .HasColumnType("TEXT")
                        .HasColumnName("school_class_id");

                    b.HasIndex("SchoolClassId")
                        .HasDatabaseName("ix_students_school_class_id");

                    b.ToTable("students", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Teacher", b =>
                {
                    b.HasBaseType("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User");

                    b.ToTable("teachers", (string)null);
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Attendance", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Lesson", "Lesson")
                        .WithMany("Attendances")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendances_lessons_lesson_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_attendances_students_student_id");

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.ClassSupervision", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.SchoolClass", "SchoolClass")
                        .WithMany("ClassSupervisions")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_class_supervisions_school_classes_class_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Teacher", "Teacher")
                        .WithMany("ClassSupervisions")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_class_supervisions_teachers_teacher_id");

                    b.Navigation("SchoolClass");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Homework", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Lesson", "Lesson")
                        .WithMany("Homeworks")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_homeworks_lessons_lesson_id");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.HomeworkSubmission", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Homework", "Homework")
                        .WithMany("Submissions")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_homework_submissions_homeworks_homework_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.HomeworkMark", "HomeworkMark")
                        .WithOne("HomeworkSubmission")
                        .HasForeignKey("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.HomeworkSubmission", "HomeworkMarkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_homework_submissions_homework_marks_homework_mark_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", "Student")
                        .WithMany("HomeworkSubmissions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_homework_submissions_students_student_id");

                    b.Navigation("Homework");

                    b.Navigation("HomeworkMark");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Lesson", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.SchoolClass", "SchoolClass")
                        .WithMany("Lessons")
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_lessons_school_classes_school_class_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_lessons_subjects_subject_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Teacher", "Teacher")
                        .WithMany("Lessons")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_lessons_teachers_teacher_id");

                    b.Navigation("SchoolClass");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.SchoolClass", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("fk_school_classes_teachers_teacher_id");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.StudentParent", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Parent", "Parent")
                        .WithMany("StudentParents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_student_parents_parents_parent_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", "Student")
                        .WithMany("StudentParents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_student_parents_students_student_id");

                    b.Navigation("Parent");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.UserRole", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_roles_role_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_users_user_id");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.HomeworkMark", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", "Student")
                        .WithMany("HomeworkMarks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_marks_students_student_id");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.LessonMark", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Lesson", "Lesson")
                        .WithMany("LessonMarks")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_marks_lessons_lesson_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", "Student")
                        .WithMany("LessonMarks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_marks_students_student_id");

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Parent", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Parent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_parents_users_id");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_students_users_id");

                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.SchoolClass", "SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_students_school_classes_school_class_id");

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Teacher", b =>
                {
                    b.HasOne("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Teacher", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_teachers_users_id");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Homework", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Lesson", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Homeworks");

                    b.Navigation("LessonMarks");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.SchoolClass", b =>
                {
                    b.Navigation("ClassSupervisions");

                    b.Navigation("Lessons");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Subject", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.HomeworkMark", b =>
                {
                    b.Navigation("HomeworkSubmission")
                        .IsRequired();
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Parent", b =>
                {
                    b.Navigation("StudentParents");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Student", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("HomeworkMarks");

                    b.Navigation("HomeworkSubmissions");

                    b.Navigation("LessonMarks");

                    b.Navigation("StudentParents");
                });

            modelBuilder.Entity("Edu_System_BackEnd.Edu_System_BackEnd.Core.Entities.Teacher", b =>
                {
                    b.Navigation("ClassSupervisions");

                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
