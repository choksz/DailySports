using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class AddedTeamLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StageTeams");

            migrationBuilder.CreateTable(
                name: "TeamLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamListTeams",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamListTeams", x => new { x.TeamId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_TeamListTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamListTeams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "TeamListId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamListId",
                table: "Teams",
                column: "TeamListId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamListTeams_TeamId",
                table: "TeamListTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamListTeams_TournamentId",
                table: "TeamListTeams",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamLists_TeamListId",
                table: "Teams",
                column: "TeamListId",
                principalTable: "TeamLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamLists_TeamListId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamListId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamListId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TeamLists");

            migrationBuilder.DropTable(
                name: "TeamListTeams");

            migrationBuilder.CreateTable(
                name: "StageTeams",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageTeams", x => new { x.TeamId, x.StageId });
                    table.ForeignKey(
                        name: "FK_StageTeams_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StageTeams_StageId",
                table: "StageTeams",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_StageTeams_TeamId",
                table: "StageTeams",
                column: "TeamId");
        }
    }
}
