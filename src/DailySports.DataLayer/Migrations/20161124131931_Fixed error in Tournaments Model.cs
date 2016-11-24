using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class FixederrorinTournamentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamListTeams_Tournaments_TournamentsId",
                table: "TeamListTeams");

            migrationBuilder.DropIndex(
                name: "IX_TeamListTeams_TournamentsId",
                table: "TeamListTeams");

            migrationBuilder.DropColumn(
                name: "TournamentsId",
                table: "TeamListTeams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentsId",
                table: "TeamListTeams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamListTeams_TournamentsId",
                table: "TeamListTeams",
                column: "TournamentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamListTeams_Tournaments_TournamentsId",
                table: "TeamListTeams",
                column: "TournamentsId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
