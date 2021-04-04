using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HrManagementAlpha;

namespace HrManagementAlpha.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MyConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public virtual DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
        public virtual DbSet<LeaveStatistics> LeaveStatistics { get; set; }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<EmployeeCertification> EmployeeCertifications { get; set; }
        public virtual DbSet<EmployeeEducation> EmployeeEducations { get; set; }
        public virtual DbSet<EmployeeEmploymentHistory> EmployeeEmploymentHistories { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Thana> Thanas { get; set; }
        public virtual DbSet<IDCardType> IDCardTypes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuss { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ConveyanceBillRow> ConveyanceBillRows { get; set; }
        public virtual DbSet<ConveyanceBill> ConveyanceBills { get; set; }
        public virtual DbSet<ConveyanceStatu> ConveyanceStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Designation>()
                .Property(e => e.DesignationName)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.DISTRICT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.REMARKS)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Persons)
                .WithOptional(e => e.District)
                .HasForeignKey(e => e.DistrictId);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Persons1)
                .WithOptional(e => e.District1)
                .HasForeignKey(e => e.PresentDistrictId);

            modelBuilder.Entity<EducationLevel>()
                .HasMany(e => e.EmployeeEducations)
                .WithOptional(e => e.EducationLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeEducation>()
                .Property(e => e.MarksPercent)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EmployeeEducation>()
                .Property(e => e.Grade)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EmployeeEducation>()
                .Property(e => e.Scale)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EmployeeEmploymentHistory>()
                .Property(e => e.Responsibilities)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeCertifications)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeEducations)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeEmploymentHistories)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeTrainings)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeTraining>()
                .Property(e => e.Country)
                .IsFixedLength();

            modelBuilder.Entity<EmployeeTraining>()
                .Property(e => e.Location)
                .IsFixedLength();

            modelBuilder.Entity<Gender>()
                .Property(e => e.GenderName)
                .IsUnicode(false);

            modelBuilder.Entity<Thana>()
                .Property(e => e.THANA_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Thana>()
                .Property(e => e.REMARKS)
                .IsUnicode(false);

            modelBuilder.Entity<Thana>()
                .HasMany(e => e.Persons)
                .WithOptional(e => e.Thana)
                .HasForeignKey(e => e.ThanaId);

            modelBuilder.Entity<Thana>()
                .HasMany(e => e.Persons1)
                .WithOptional(e => e.Thana1)
                .HasForeignKey(e => e.PresentThanaId);
            modelBuilder.Entity<ConveyanceBill>()
                .HasMany(e => e.ConveyanceBillRows)
                .WithRequired(e => e.ConveyanceBill)
                .HasForeignKey(e => e.ConveyanceBillId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ConveyanceBills)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.SubmittedBy)
                .WillCascadeOnDelete(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}