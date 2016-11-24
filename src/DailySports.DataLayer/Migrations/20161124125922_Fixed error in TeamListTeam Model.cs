using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class FixederrorinTeamListTeamModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamListTeams_Tournaments_TournamentId",
                table: "TeamListTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamListTeams",
                table: "TeamListTeams");

            migrationBuilder.DropIndex(
                name: "IX_TeamListTeams_TournamentId",
                table: "TeamListTeams");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "TeamListTeams");

            migrationBuilder.AddColumn<int>(
                name: "TeamListId",
                table: "TeamListTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TournamentsId",
                table: "TeamListTeams",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamListTeams",
                table: "TeamListTeams",
                columns: new[] { "TeamId", "TeamListId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamListTeams_TeamListId",
                table: "TeamListTeams",
                column: "TeamListId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamListTeams_TournamentsId",
                table: "TeamListTeams",
                column: "TournamentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamListTeams_TeamLists_TeamListId",
                table: "TeamListTeams",
                column: "TeamListId",
                principalTable: "TeamLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamListTeams_Tournaments_TournamentsId",
                table: "TeamListTeams",
                column: "TournamentsId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamListTeams_TeamLists_TeamListId",
                table: "TeamListTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamListTeams_Tournaments_TournamentsId",
                table: "TeamListTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamListTeams",
                table: "TeamListTeams");

            migrationBuilder.DropIndex(
                name: "IX_TeamListTeams_TeamListId",
                table: "TeamListTeams");

            migrationBuilder.DropIndex(
                name: "IX_TeamListTeams_TournamentsId",
                table: "TeamListTeams");

            migrationBuilder.DropColumn(
                name: "TeamListId",
                table: "TeamListTeams");

            migrationBuilder.DropColumn(
                name: "TournamentsId",
                table: "TeamListTeams");

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "TeamListTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamListTeams",
                table: "TeamListTeams",
                columns: new[] { "TeamId", "TournamentId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamListTeams_TournamentId",
                table: "TeamListTeams",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamListTeams_Tournaments_TournamentId",
                table: "TeamListTeams",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
