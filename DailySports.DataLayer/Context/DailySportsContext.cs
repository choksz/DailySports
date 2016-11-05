using DailySports.DataLayer.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DailySports.DataLayer.Context
{
    public class DailySportsContext : DbContext
    {
        public DailySportsContext() : base("DailySportsContext")
        {
            Database.CreateIfNotExists();

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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

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
