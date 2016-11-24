using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class AddedFKtoTeamLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "TeamLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamLists_TournamentId",
                table: "TeamLists",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLists_Tournaments_TournamentId",
                table: "TeamLists",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamLists_Tournaments_TournamentId",
                table: "TeamLists");

            migrationBuilder.DropIndex(
                name: "IX_TeamLists_TournamentId",
                table: "TeamLists");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "TeamLists");
        }
    }
}
