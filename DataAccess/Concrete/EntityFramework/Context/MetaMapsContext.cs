using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class MetaMapsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-HIGG7H3; Database=MetaMaps; Trusted_Connection=true");
        }
        //
        // Server = DESKTOP-HIGG7H3; Database=MetaMaps; Trusted_Connection=true
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Creater> Creaters { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RoadMap> RoadMaps { get; set; }
        public DbSet<RoadMapOfStep> RoadMapOfSteps { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepOfBranch> StepOfBranches { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
    }
}
