using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace Repository.Configuration
{
    public class MentoriaContext : DbContext
    {
        public MentoriaContext(DbContextOptions<MentoriaContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
