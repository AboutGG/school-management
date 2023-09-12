using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class SchoolContext : DbContext
{
    #region DbSets
    public DbSet<Registry> Registries { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<TeacherSubjectClassroom> TeachersSubjectsClassrooms{ get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<StudentExam> RegistryExams { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    #endregion
    
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Seeder.SeedData(modelBuilder);
        
        #region Uniques

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Classroom>()
            .HasIndex(c => c.Name)
            .IsUnique();

        #endregion

        #region User relations

        /// <summary> User relation with Registry one-to-one</summary>
        modelBuilder.Entity<User>()
            .HasOne<Registry>(u => u.Registry)
            .WithOne(r => r.User)
            .HasForeignKey<User>(t => t.RegistryId);

        #endregion

        #region Student relations
        
        modelBuilder.Entity<Student>().HasKey(s => new { s.UserId, s.ClassroomId });
        
        /// <summary> Student relation with user one-to-one</summary>
        modelBuilder.Entity<Student>()
            .HasOne<User>(s => s.User)
            .WithOne(u => u.Student)
            .HasForeignKey<Student>(s => s.UserId);

        /// <summary> Student relation with classroom one-to-many</summary>
        modelBuilder.Entity<Student>()
            .HasOne<Classroom>(s => s.Classroom)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.ClassroomId);
            
        #endregion

        #region TeacherSubjectClassroom relations

        ///<summary> TrecherSubject relation many-to-many</summary>

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<User>(ts => ts.User)
            .WithMany(t => t.TeachersSubjectsClassrooms)
            .HasForeignKey(ts => ts.UserId);

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Subject>(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjectClassrooms)
            .HasForeignKey(ts => ts.SubjectId);

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Classroom>(ts => ts.Classroom)
            .WithMany(c => c.TeachersSubjectsClassrooms)
            .HasForeignKey(ts => ts.ClassroomId);

        #endregion

        #region Exam relations
        
        ///<summary> Exam relation with subject one-to-one </summary>

        modelBuilder.Entity<Exam>()
            .HasOne(e => e.TeacherSubjectClassroom)
            .WithMany(tsc => tsc.Exam)
            .HasForeignKey(e => e.TeacherSubjectClassroomId);

        #endregion

        #region StudentExam relations

        ///<summary> StudentExam relation many-to-many</summary>
        modelBuilder.Entity<StudentExam>().HasKey(re => new { re.ExamId, re.UserId });

        modelBuilder.Entity<StudentExam>()
            .HasOne<Exam>(re => re.Exam)
            .WithMany(e => e.StudentExams)
            .HasForeignKey(re => re.ExamId);

        modelBuilder.Entity<StudentExam>()
            .HasOne<User>(re => re.User)
            .WithMany(u => u.StudentsExams)
            .HasForeignKey(re => re.UserId);

        #endregion
        
        modelBuilder.Entity<UserRole>().HasKey(re => new { re.UserId, re.RoleId });

        
        modelBuilder.Entity<UserRole>()
            .HasOne<User>(ts => ts.User)
            .WithMany(t => t.UsersRoles)
            .HasForeignKey(ts => ts.UserId);
        
        modelBuilder.Entity<UserRole>()
            .HasOne<Role>(ts => ts.Role)
            .WithMany(s => s.UsersRoles)
            .HasForeignKey(ts => ts.RoleId);
    }
}