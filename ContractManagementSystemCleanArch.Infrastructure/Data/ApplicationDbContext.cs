using CMS.Domain.Entities;
using CMS.Domain.Entities.Approval;
using CMS.Domain.Entities.Authentication;
using CMS.Domain.Entities.Contract;
using CMS.Domain.Entities.Contract.ContractManagement;
using CMS.Domain.Entities.ContractManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        // Constructor that accepts DbContextOptions and passes it to the base class constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Approver> Approvers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Negotiation> Negotiations { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<ContractParty> ContractParties { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<TerminationClause> TerminationClauses { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<ContractsReport> Contract { get; set; }
        public DbSet<ContractDocument> ContractDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContractsReport>().HasData(
                new ContractsReport
                {
                    Id = 1,
                    Status = "pending",
                    Type = "type1",
                    Department = "dept1",
                    Value = 1000m,
                },
                new ContractsReport
                {
                    Id = 2,
                    Status = "active",
                    Type = "type2",
                    Department = "dept2",
                    Value = 2000m,
                },
                new ContractsReport
                {
                    Id = 3,
                    Status = "rejected",
                    Type = "type1",
                    Department = "dept3",
                    Value = 3000m,
                },
                new ContractsReport
                {
                    Id = 4,
                    Status = "expired",
                    Type = "type2",
                    Department = "dept4",
                    Value = 4000m,
                },
                new ContractsReport
                {
                    Id = 5,
                    Status = "pending",
                    Type = "type1",
                    Department = "dept1",
                    Value = 5000m,
                },
                new ContractsReport
                {
                    Id = 6,
                    Status = "active",
                    Type = "type2",
                    Department = "dept2",
                    Value = 6000m,
                },
                new ContractsReport
                {
                    Id = 7,
                    Status = "rejected",
                    Type = "type1",
                    Department = "dept3",
                    Value = 7000m,
                },
                new ContractsReport
                {
                    Id = 8,
                    Status = "expired",
                    Type = "type2",
                    Department = "dept4",
                    Value = 8000m,
                },
                new ContractsReport
                {
                    Id = 9,
                    Status = "pending",
                    Type = "type1",
                    Department = "dept1",
                    Value = 9000m,
                },
                new ContractsReport
                {
                    Id = 10,
                    Status = "active",
                    Type = "type2",
                    Department = "dept2",
                    Value = 10000m,
                }
            );
        }

        // DbSet for Collaboration entity
        public DbSet<Collaboration> Collaboration { get; set; }

        
    }


}

