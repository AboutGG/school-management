using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class SchoolContext : DbContext
{
    #region DbSets
    public DbSet<Registry> Registries { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<TeacherSubjectClassroom> TeachersSubjectsClassrooms{ get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<RegistryExam> RegistryExams { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    #endregion
    
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Uniques

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Classroom>()
            .HasIndex(c => c.Name)
            .IsUnique();

        #endregion

        #region Teacher relations

        /// <summary> Teacher relation with user one-to-one</summary>
        modelBuilder.Entity<Teacher>()
            .HasOne<User>(t => t.User)
            .WithOne(u => u.Teacher)
            .HasForeignKey<Teacher>(t => t.UserId);

        /// <summary> Teacher relation with registry one-to-one</summary>
        modelBuilder.Entity<Teacher>()
            .HasOne<Registry>(t => t.Registry)
            .WithOne(r => r.Teacher)
            .HasForeignKey<Teacher>(t => t.RegistryId);

        #endregion

        #region Student relations

        /// <summary> Student relation with user one-to-one</summary>
        modelBuilder.Entity<Student>()
            .HasOne<User>(s => s.User)
            .WithOne(u => u.Student)
            .HasForeignKey<Student>(s => s.UserId);

        /// <summary> Student relation with registry one-to-one</summary>
        modelBuilder.Entity<Student>()
            .HasOne<Registry>(s => s.Registry)
            .WithOne(r => r.Student)
            .HasForeignKey<Student>(s => s.RegistryId);

        /// <summary> Student relation with classroom one-to-many</summary>
        modelBuilder.Entity<Student>()
            .HasOne<Classroom>(s => s.Classroom)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.ClassroomId);
            
        #endregion

        #region TeacherSubjectClassroom relations

        ///<summary> TrecherSubject relation many-to-many</summary>
        modelBuilder.Entity<TeacherSubjectClassroom>().HasKey(ts => new { ts.TeacherId, ts.SubjectId, ts.ClassroomId });

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Teacher>(ts => ts.Teacher)
            .WithMany(t => t.TeacherSubjectsClassrooms)
            .HasForeignKey(ts => ts.TeacherId);

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Subject>(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjects)
            .HasForeignKey(ts => ts.SubjectId);

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Classroom>(ts => ts.Classroom)
            .WithMany(c => c.TeacherSubjects)
            .HasForeignKey(ts => ts.ClassroomId);

        #endregion

        #region Exam relations

        ///<summary> Exam relation with subject one-to-many </summary>
        modelBuilder.Entity<Exam>()
            .HasOne<Subject>(e => e.Subject)
            .WithMany(s => s.Exams)
            .HasForeignKey(e => e.SubjectId);

        #endregion

        #region RegistryExam relations

        ///<summary> RegistryExam relation many-to-many</summary>
        modelBuilder.Entity<RegistryExam>().HasKey(re => new { re.ExamId, re.RegistryId });

        modelBuilder.Entity<RegistryExam>()
            .HasOne<Exam>(re => re.Exam)
            .WithMany(e => e.RegistryExams)
            .HasForeignKey(re => re.ExamId);

        modelBuilder.Entity<RegistryExam>()
            .HasOne<Registry>(re => re.Registry)
            .WithMany(r => r.RegistryExams)
            .HasForeignKey(re => re.RegistryId);

        #endregion
    }
}