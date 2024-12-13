using ContractManagementSystem.Models;
using ContractManagementSystem.Models.ContractManagement;
using ContractManagementSystem.Models.ContractManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClauseLibrary = ContractManagementSystem.Models.ClauseLibrary;



namespace ContractManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, ApplicationRole, string>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser>  AppUsers { get; set; } 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VendorCompany> VendorCompanies { get; set; }
        public DbSet<VendorIndividual> VendorIndividuals { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Approval> Approval { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ApprovalProcess> ApprovalProcesses { get; set; }
        public DbSet<TerminationClause> TerminationClauses { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<ContractParty> ContractParties { get; set; }
        public DbSet<ContractDocument> ContractDocuments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Negotiations> Negotiations { get; set; }
        public DbSet<ClauseLibrary> ClauseLibrary { get; set; }
        public DbSet<ContractTemplate> ContractTemplates { get; set; }  
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Existing RolePrivilege configuration
            builder.Entity<RolePrivilege>()
                .HasKey(rp => new { rp.RoleId, rp.PrivilegeId });

            builder.Entity<RolePrivilege>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePrivileges)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePrivilege>()
                .HasOne(rp => rp.Privilege)
                .WithMany(p => p.RolePrivileges)
                .HasForeignKey(rp => rp.PrivilegeId);

            // Configure cascade delete for Contract and ContractParties
            builder.Entity<Contract>()
                .HasMany(c => c.ContractParties)
                .WithOne(cp => cp.Contract)
                .HasForeignKey(cp => cp.ContractId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Collaborator>()
            .HasKey(c => c.Id); // Primary key configuration

            // Repeat similar configurations for other related entities if needed
        }



    }
}
