

using ISMIEEmploymentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ISMIEEmploymentApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateAddress> CandidateAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Addresses)
                .WithOne(a => a.Candidate)
                .HasForeignKey(a => a.CandidateId);
        }
    }
}
