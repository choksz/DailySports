using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class AddedmanytomanyrelationshipbetweenTeamandTeamList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Stages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stages_TeamId",
                table: "Stages",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Teams_TeamId",
                table: "Stages",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Teams_TeamId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_TeamId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Stages");
        }
    }
}
