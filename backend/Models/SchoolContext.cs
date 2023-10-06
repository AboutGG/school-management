using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Models;

public class SchoolContext : DbContext
{
    #region DbSets

    public DbSet<Registry> Registries { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<TeacherSubjectClassroom> TeachersSubjectsClassrooms { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<StudentExam> RegistryExams { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Circular> Circulars { get; set; }
    public DbSet<PromotionHistory> PromotionsHistories { get; set; }

    #endregion

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Seeder.SeedData(modelBuilder);

        #region Filters

        modelBuilder.Entity<User>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<Registry>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<Student>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<Teacher>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<Classroom>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<Exam>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<Subject>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<StudentExam>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<TeacherSubjectClassroom>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<Circular>().HasQueryFilter(el => el.DeletedAt == null);
        modelBuilder.Entity<PromotionHistory>().HasQueryFilter(el => el.DeletedAt == null);
        
        #endregion

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

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Teacher>(ts => ts.Teacher)
            .WithMany(t => t.TeachersSubjectsClassrooms)
            .HasForeignKey(ts => ts.TeacherId);

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Subject>(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjectsClassrooms)
            .HasForeignKey(ts => ts.SubjectId);

        modelBuilder.Entity<TeacherSubjectClassroom>()
            .HasOne<Classroom>(ts => ts.Classroom)
            .WithMany(c => c.TeacherSubjectsClassrooms)
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
        modelBuilder.Entity<StudentExam>().HasKey(re => new { re.ExamId, re.StudentId });

        modelBuilder.Entity<StudentExam>()
            .HasOne<Exam>(re => re.Exam)
            .WithMany(e => e.StudentExams)
            .HasForeignKey(re => re.ExamId);

        modelBuilder.Entity<StudentExam>()
            .HasOne<Student>(re => re.Student)
            .WithMany(r => r.StudentExams)
            .HasForeignKey(re => re.StudentId);

        #endregion

        #region PromotionHistory

        //PromotionHistory relation many-to-many

        modelBuilder.Entity<PromotionHistory>()
            .HasOne<Classroom>(ph => ph.PreviousClassroom)
            .WithMany(c => c.PromotionHistories)
            .HasForeignKey(ph => ph.PreviousClassroomId);

        modelBuilder.Entity<PromotionHistory>()
            .HasOne<Student>(ph => ph.Student)
            .WithMany(s => s.PromotionHistories)
            .HasForeignKey(ph => ph.StudentId);
        

        #endregion
    }

    #region Overrides

    public override int SaveChanges()
    {
        OnBeforeChange();
        return base.SaveChanges();
    }

    #endregion


    #region Private Methods

    private void OnBeforeChange()
    {
        foreach (var entries in ChangeTracker.Entries())
        {
            if (entries.Entity is Deleted entity)
            {
                if (entries.State == EntityState.Deleted)
                {
                    entity.DeletedAt = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime());
                    entries.State = EntityState.Modified;
                }
            }
        }
    }

    #endregion
}