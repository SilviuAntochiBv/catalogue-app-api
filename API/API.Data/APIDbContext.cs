using Microsoft.EntityFrameworkCore;
using API.Domain.Entities;

namespace API.Data
{
    public class APIDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public APIDbContext() : base()
        {
        }

        public APIDbContext(DbContextOptions<APIDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("classes");

                entity.HasIndex(e => e.Name)
                    .HasName("unique_class_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity
                    .Property(e => e.Id)
                    .HasColumnName("id");

                entity
                    .Property(e => e.Name)
                    .HasColumnName("name");

                entity
                    .HasIndex(e => e.Name)
                    .HasName("unique_course_name")
                    .IsUnique();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subjects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Name)
                    .HasName("unique_subject_name")
                    .IsUnique();
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teachers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ClassCourseAssociation>(entity =>
            {
                entity.ToTable("classes_in_course");

                entity.HasKey(key => new { key.ClassId, key.CourseId });

                entity
                    .Property(e => e.ClassId)
                    .IsRequired()
                    .HasColumnName("class_id");

                entity
                    .Property(e => e.CourseId)
                    .IsRequired()
                    .HasColumnName("course_id");
            });

            modelBuilder.Entity<TeacherSubjectAssociation>(entity =>
            {
                entity.ToTable("teachers_in_subjects");

                entity.HasKey(key => new { key.SubjectId, key.TeacherId });

                entity
                    .Property(e => e.SubjectId)
                    .IsRequired()
                    .HasColumnName("subject_id");

                entity
                    .Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnName("teacher_id");
            });
        }
    }
}
