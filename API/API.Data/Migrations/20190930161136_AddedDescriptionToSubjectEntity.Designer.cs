﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Data.Migrations
{
    [DbContext(typeof(APIDbContext))]
    [Migration("20190930161136_AddedDescriptionToSubjectEntity")]
    partial class AddedDescriptionToSubjectEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("API.Domain.Entities.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("unique_class_name");

                    b.ToTable("classes");
                });

            modelBuilder.Entity("API.Domain.Entities.ClassCourseAssociation", b =>
                {
                    b.Property<int>("ClassId")
                        .HasColumnName("class_id");

                    b.Property<int>("CourseId")
                        .HasColumnName("course_id");

                    b.HasKey("ClassId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("classes_in_course");
                });

            modelBuilder.Entity("API.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int?>("SubjectId");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("unique_course_name");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("API.Domain.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<short>("Age")
                        .HasColumnName("age");

                    b.Property<int?>("AssociatedClassId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AssociatedClassId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("API.Domain.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("unique_subject_name");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("API.Domain.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("teachers");
                });

            modelBuilder.Entity("API.Domain.Entities.TeacherSubjectAssociation", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnName("subject_id");

                    b.Property<int>("TeacherId")
                        .HasColumnName("teacher_id");

                    b.HasKey("SubjectId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("teachers_in_subjects");
                });

            modelBuilder.Entity("API.Domain.Entities.ClassCourseAssociation", b =>
                {
                    b.HasOne("API.Domain.Entities.Class", "Class")
                        .WithMany("AssociatedCourses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Domain.Entities.Course", "Course")
                        .WithMany("AssociatedClasses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Domain.Entities.Course", b =>
                {
                    b.HasOne("API.Domain.Entities.Subject", "Subject")
                        .WithMany("AssociatedCourses")
                        .HasForeignKey("SubjectId");

                    b.HasOne("API.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("API.Domain.Entities.Student", b =>
                {
                    b.HasOne("API.Domain.Entities.Class", "AssociatedClass")
                        .WithMany("EnrolledStudents")
                        .HasForeignKey("AssociatedClassId");
                });

            modelBuilder.Entity("API.Domain.Entities.TeacherSubjectAssociation", b =>
                {
                    b.HasOne("API.Domain.Entities.Subject", "Subject")
                        .WithMany("Teachers")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
