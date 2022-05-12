using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudentManagementV2.Core.Models
{
    public partial class StudentManagementContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public StudentManagementContext()
        {
        }

        public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignClass> AssignClasses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Loggin> Loggins { get; set; }
        public new virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectRegister> SubjectRegisters { get; set; }
        public new virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
                //You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration
                //- see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=StudentManagement;User ID=sa; Password=password123#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AssignClass>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("ASSIGN_CLASS");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CLASS_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("CLASS_NAME");

                entity.Property(e => e.DeptId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DEPT_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.AssignClasses)
                    .HasForeignKey(d => d.DeptId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ASSIGN_CLASS_DEPARTMENT");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId);

                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.DeptId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DEPT_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.DeptName)
                    .HasMaxLength(50)
                    .HasColumnName("DEPT_NAME");
            });

            modelBuilder.Entity<Loggin>(entity =>
            {
                entity.ToTable("LOGGIN");

                entity.Property(e => e.LogginId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("LOGGIN_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.LogginName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOGGIN_NAME");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Loggins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LOGGIN_USER");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("SEMESTER");

                entity.Property(e => e.SemesterId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SEMESTER_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.SemesterName)
                    .HasMaxLength(50)
                    .HasColumnName("SEMESTER_NAME");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("SUBJECT");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(50)
                    .HasColumnName("SUBJECT_NAME");
            });

            modelBuilder.Entity<SubjectRegister>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectId, e.SemesterId, e.Year });

                entity.ToTable("SUBJECT_REGISTER");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.SemesterId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SEMESTER_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Year).HasColumnName("YEAR");

                entity.Property(e => e.Score1)
                    .HasColumnType("numeric(4, 2)")
                    .HasColumnName("SCORE_1");

                entity.Property(e => e.Score2)
                    .HasColumnType("numeric(4, 2)")
                    .HasColumnName("SCORE_2");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.SubjectRegisters)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_SUBJECT_REGISTER_SEMESTER");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SubjectRegisters)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_SUBJECT_REGISTER_USER");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectRegisters)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_SUBJECT_REGISTER_SUBJECT");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("date")
                    .HasColumnName("BIRTHDATE");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.IdentityId)
                    .HasMaxLength(100)
                    .HasColumnName("IDENTITY_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_USER_ROLE");

                entity.HasDiscriminator(e => e.RoleId)
                .HasValue<Student>("R01")
                .HasValue<Instructor>("R02")
                .HasValue<Admin>("R03");
            });

            modelBuilder.Entity<Student>(entity => {

                entity.Property(e => e.ClassId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CLASS_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_USER_ASSIGN_CLASS");

            });

            modelBuilder.Entity<Instructor>(entity => {

                entity.Property(e => e.DeptId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DEPT_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.DeptId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_USER_DEPARTMENT");

            });

            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
