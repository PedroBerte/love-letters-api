using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Repository.Context
{
    public class LoveLettersContext : DbContext
    {
        public LoveLettersContext(DbContextOptions<LoveLettersContext> options)
        : base(options)
        {
        }
        public DbSet<users> users { get; set; }
        public DbSet<invites> invites { get; set; }
        public DbSet<relationship> relationship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<users>()
            .HasKey(u => u.uid);

            modelBuilder.Entity<relationship>()
            .HasKey(u => u.id);

            modelBuilder.Entity<invites>()
            .HasKey(u => u.id);
        }
    }

    public class users
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? profilePhoto { get; set; }
        public string? partnerUID { get; set; }
        public string? partnerName { get; set; }
        public bool havePartner { get; set; }
    }


    public class invites
    {
        public int id { get; set; }
        public bool inviteAccepted { get; set; }
        public DateTime inviteDate { get; set; }
        public string uidInvited { get; set; }
        public string uidInviter { get; set; }
    }

    public class relationship
    {
        public int id { get; set; }
        public string partnerUid1 { get; set; }
        public string partnerUid2 { get; set; }
        public string status { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
