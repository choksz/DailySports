using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class RemodelTournaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PrizePools_Teams_TeamId",
                table: "PrizePools");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_GroupStages_GroupStageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_GroupStageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_PrizePools_TeamId",
                table: "PrizePools");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "MainEvent",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Qualifiers",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Stream",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "GroupStageId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "PrizePools");

            migrationBuilder.DropColumn(
                name: "Prize",
                table: "PrizePools");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PrizePools");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "GroupStages");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Flag = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Flag = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "PlaceEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Amount = table.Column<double>(nullable: false),
                    From = table.Column<int>(nullable: false),
                    PrizePoolId = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceEntries_PrizePools_PrizePoolId",
                        column: x => x.PrizePoolId,
                        principalTable: "PrizePools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamLists_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stream",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    LanguageCode = table.Column<string>(nullable: true),
                    Main = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TournamentId = table.Column<int>(nullable: false),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stream", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stream_Languages_LanguageCode",
                        column: x => x.LanguageCode,
                        principalTable: "Languages",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stream_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TeamListId = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_TeamLists_TeamListId",
                        column: x => x.TeamListId,
                        principalTable: "TeamLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stages_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "PrizePoolId",
                table: "Tournaments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamListId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nick",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoreA",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreB",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tournaments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Overview",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_PrizePoolId",
                table: "Tournaments",
                column: "PrizePoolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CountryId",
                table: "Teams",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamListId",
                table: "Teams",
                column: "TeamListId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CountryId",
                table: "Players",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StageId",
                table: "Matches",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceEntries_PrizePoolId",
                table: "PlaceEntries",
                column: "PrizePoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_TeamListId",
                table: "Stages",
                column: "TeamListId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_TournamentId",
                table: "Stages",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stream_LanguageCode",
                table: "Stream",
                column: "LanguageCode");

            migrationBuilder.CreateIndex(
                name: "IX_Stream_TournamentId",
                table: "Stream",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLists_TournamentId",
                table: "TeamLists",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Stages_StageId",
                table: "Matches",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_CountryId",
                table: "Players",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamLists_TeamListId",
                table: "Teams",
                column: "TeamListId",
                principalTable: "TeamLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_PrizePools_PrizePoolId",
                table: "Tournaments",
                column: "PrizePoolId",
                principalTable: "PrizePools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Stages_StageId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_CountryId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamLists_TeamListId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_PrizePools_PrizePoolId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_PrizePoolId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CountryId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_GameId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamListId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_CountryId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Matches_StageId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "PrizePoolId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamListId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Nick",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ScoreA",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ScoreB",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "PlaceEntries");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Stream");

            migrationBuilder.DropTable(
                name: "TeamLists");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.CreateTable(
                name: "GroupStages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupStages_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tournaments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Tournaments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainEvent",
                table: "Tournaments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Tournaments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Qualifiers",
                table: "Tournaments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Stream",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupStageId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "PrizePools",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prize",
                table: "PrizePools",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "PrizePools",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tournaments",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Overview",
                table: "Tournaments",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GroupStageId",
                table: "Teams",
                column: "GroupStageId");

            migrationBuilder.CreateIndex(
                name: "IX_PrizePools_TeamId",
                table: "PrizePools",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStages_TournamentId",
                table: "GroupStages",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrizePools_Teams_TeamId",
                table: "PrizePools",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_GroupStages_GroupStageId",
                table: "Teams",
                column: "GroupStageId",
                principalTable: "GroupStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
