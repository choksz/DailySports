using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DailySports.DataLayer.Context;

namespace DailySports.DataLayer.Migrations
{
    [DbContext(typeof(DailySportsContext))]
    partial class DailySportsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("DailySports.DataLayer.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("UserId");

                    b.Property<int?>("VideosId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VideosId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Currency");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("EventImage");

                    b.Property<string>("Location");

                    b.Property<decimal>("Price");

                    b.Property<string>("Region");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Tag");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("ticketid");

                    b.HasKey("Id");

                    b.HasIndex("ticketid");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.EventComments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentId");

                    b.Property<int>("EventId");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("EventId");

                    b.ToTable("EventComments");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.EventImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<string>("File");

                    b.Property<string>("Tag")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventImages");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameImage");

                    b.Property<string>("LiveStreamUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.GroupStages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("GroupStages");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("TeamAId");

                    b.Property<int>("TeamBId");

                    b.Property<int>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("TeamAId");

                    b.HasIndex("TeamBId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("GameId");

                    b.Property<string>("NewsImage");

                    b.Property<string>("Tag");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("TournamentId");

                    b.Property<int>("status");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GameId");

                    b.HasIndex("TournamentId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.NewsComments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentId");

                    b.Property<int>("NewsId");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsComments");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.PetOfTheWeek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FunFact");

                    b.Property<bool>("Gender");

                    b.Property<string>("Owner");

                    b.Property<string>("PetImage");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("PetOfTheWeek");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Name");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Privilge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Privilge");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.PrizePool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<int>("Prize");

                    b.Property<int>("TeamId");

                    b.Property<int>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TournamentId");

                    b.ToTable("PrizePools");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupStageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GroupStageId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.TeamMatches", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchId");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMatches");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Notes");

                    b.Property<decimal?>("Price");

                    b.Property<long>("Quantity");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.TicketType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TicketTypes");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Tournaments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Format")
                        .IsRequired();

                    b.Property<int>("GameId");

                    b.Property<string>("MainEvent")
                        .IsRequired();

                    b.Property<string>("Overview")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<string>("Qualifiers")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Stream");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("TournamentImage");

                    b.Property<string>("URL");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Biography");

                    b.Property<string>("Email");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("SecurityCode");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.VideoComments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentId");

                    b.Property<int>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("VideoId");

                    b.ToTable("VideosComments");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Videos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("GameId");

                    b.Property<string>("Tag");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("TournamentId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GameId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Comments", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("DailySports.DataLayer.Model.Videos")
                        .WithMany("Comments")
                        .HasForeignKey("VideosId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Event", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Ticket", "ticket")
                        .WithMany()
                        .HasForeignKey("ticketid");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.EventComments", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Comments", "Comments")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("DailySports.DataLayer.Model.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.EventImage", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Event", "Event")
                        .WithMany("Images")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.GroupStages", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Tournaments", "Tournament")
                        .WithMany("GroupStages")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Match", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Team", "TeamA")
                        .WithMany()
                        .HasForeignKey("TeamAId");

                    b.HasOne("DailySports.DataLayer.Model.Team", "TeamB")
                        .WithMany()
                        .HasForeignKey("TeamBId");

                    b.HasOne("DailySports.DataLayer.Model.Tournaments", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.News", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("DailySports.DataLayer.Model.Category", "category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("DailySports.DataLayer.Model.Game", "game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("DailySports.DataLayer.Model.Tournaments", "Tournament")
                        .WithMany("News")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.NewsComments", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Comments", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("DailySports.DataLayer.Model.News", "News")
                        .WithMany()
                        .HasForeignKey("NewsId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Player", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Team", "team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.PrizePool", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.HasOne("DailySports.DataLayer.Model.Tournaments", "Tournament")
                        .WithMany("PrizePool")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Team", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.GroupStages", "GroupStage")
                        .WithMany("Team")
                        .HasForeignKey("GroupStageId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.TeamMatches", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId");

                    b.HasOne("DailySports.DataLayer.Model.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Ticket", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.TicketType", "ticketType")
                        .WithMany()
                        .HasForeignKey("TicketId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Tournaments", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.VideoComments", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Comments", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("DailySports.DataLayer.Model.Videos", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("DailySports.DataLayer.Model.Videos", b =>
                {
                    b.HasOne("DailySports.DataLayer.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("DailySports.DataLayer.Model.Game", "Game")
                        .WithMany("Videos")
                        .HasForeignKey("GameId");

                    b.HasOne("DailySports.DataLayer.Model.Tournaments", "Tournament")
                        .WithMany("Videos")
                        .HasForeignKey("TournamentId");
                });
        }
    }
}
