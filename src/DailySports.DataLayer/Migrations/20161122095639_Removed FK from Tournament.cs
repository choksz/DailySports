using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class RemovedFKfromTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_PrizePools_PrizePoolId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_PrizePoolId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_PrizePools_TournamentId",
                table: "PrizePools");

            migrationBuilder.DropColumn(
                name: "PrizePoolId",
                table: "Tournaments");

            migrationBuilder.CreateIndex(
                name: "IX_PrizePools_TournamentId",
                table: "PrizePools",
                column: "TournamentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrizePools_TournamentId",
                table: "PrizePools");

            migrationBuilder.AddColumn<int>(
                name: "PrizePoolId",
                table: "Tournaments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_PrizePoolId",
                table: "Tournaments",
                column: "PrizePoolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrizePools_TournamentId",
                table: "PrizePools",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_PrizePools_PrizePoolId",
                table: "Tournaments",
                column: "PrizePoolId",
                principalTable: "PrizePools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
