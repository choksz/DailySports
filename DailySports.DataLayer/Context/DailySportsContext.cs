using DailySports.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Context
{
   public class DailySportsContext:  DbContext
    {
        public DailySportsContext(): base("DailySportsContext")
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
        public virtual DbSet<PetOfTheWeek> PetOfTheDay { get; set; }
       
        public virtual DbSet<GroupStages> GroupStages { get; set; }
        protected  override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.Game> Games { get; set; }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.User> Users { get; set; }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.EventImage> EventImages { get; set; }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.Ticket> Tickets { get; set; }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.Match> Matches { get; set; }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.Player> Players { get; set; }

        public System.Data.Entity.DbSet<DailySports.DataLayer.Model.PrizePool> PrizePools { get; set; }

    }
}
