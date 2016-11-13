using DailySports.DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.PlatformAbstractions;

namespace DailySports.DataLayer.Context
{
    public class DailySportsContext : DbContext
    {
        public DailySportsContext(DbContextOptions<DailySportsContext> options) : base(options)
        { }
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=104.155.185.35;User ID=dailysports;Password=dailysports;Database=dailysports"); 
        }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        */
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var ent in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var x in ent.GetReferencingForeignKeys()) {
                    x.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        public virtual DbSet<TicketType> TicketTypes { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Videos> Videos { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Privilge> Privilge { get; set; }
        public virtual DbSet<Tournaments> Tournaments { get; set; }
        public virtual DbSet<NewsComments> NewsComments { get; set; }
        public virtual DbSet<VideoComments> VideosComments { get; set; }
        public virtual DbSet<EventComments> EventComments { get; set; }

        public virtual DbSet<TeamMatches> TeamMatches { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<PetOfTheWeek> PetOfTheWeek { get; set; }

        public virtual DbSet<GroupStages> GroupStages { get; set; }
        
        public DbSet<Game> Games { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EventImage> EventImages { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PrizePool> PrizePools { get; set; }

    }
}
