using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class ChangedFKinTeamList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_TeamLists_TeamListId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamLists_Tournaments_TournamentId",
                table: "TeamLists");

            migrationBuilder.DropIndex(
                name: "IX_TeamLists_TournamentId",
                table: "TeamLists");

            migrationBuilder.DropIndex(
                name: "IX_Stages_TeamListId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "TeamLists");

            migrationBuilder.DropColumn(
                name: "TeamListId",
                table: "Stages");

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "TeamLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamLists_StageId",
                table: "TeamLists",
                column: "StageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLists_Stages_StageId",
                table: "TeamLists",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamLists_Stages_StageId",
                table: "TeamLists");

            migrationBuilder.DropIndex(
                name: "IX_TeamLists_StageId",
                table: "TeamLists");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "TeamLists");

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "TeamLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamListId",
                table: "Stages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamLists_TournamentId",
                table: "TeamLists",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_TeamListId",
                table: "Stages",
                column: "TeamListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_TeamLists_TeamListId",
                table: "Stages",
                column: "TeamListId",
                principalTable: "TeamLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLists_Tournaments_TournamentId",
                table: "TeamLists",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
