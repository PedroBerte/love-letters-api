using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveLetters.Repository.Context
{
    public class LoveLettersContext : DbContext
    {
        public LoveLettersContext(DbContextOptions<LoveLettersContext> options)
        : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Invites> Invites { get; set; }
        public DbSet<Relationship> Relationship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
            .HasKey(u => u.guid);

            modelBuilder.Entity<Relationship>()
            .HasKey(u => u.id);

            modelBuilder.Entity<Invites>()
            .HasKey(u => u.id);
        }
    }

    public class Users
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? profilePhoto { get; set; }
        public string? partnerGuid { get; set; }
        public string? partnerName { get; set; }
        public bool havePartner { get; set; }
        [NotMapped]
        public string? jwtToken { get; set; }
    }


    public class Invites
    {
        public int id { get; set; }
        public bool inviteAccepted { get; set; }
        public DateTime inviteDate { get; set; }
        public string guidInvited { get; set; }
        public string guidInviter { get; set; }
    }

    public class Relationship
    {
        public int id { get; set; }
        public string partnerGuid1 { get; set; }
        public string partnerGuid2 { get; set; }
        public string status { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
