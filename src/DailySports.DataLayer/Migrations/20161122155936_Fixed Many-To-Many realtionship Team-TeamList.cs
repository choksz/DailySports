using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class FixedManyToManyrealtionshipTeamTeamList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Teams_TeamId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamLists_TeamListId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamListId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Stages_TeamId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "TeamListId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Stages");

            migrationBuilder.DropTable(
                name: "TeamLists");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    Name = table.Column<string>(nullable: true),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamLists_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "TeamListId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Stages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamListId",
                table: "Teams",
                column: "TeamListId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_TeamId",
                table: "Stages",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLists_StageId",
                table: "TeamLists",
                column: "StageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Teams_TeamId",
                table: "Stages",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamLists_TeamListId",
                table: "Teams",
                column: "TeamListId",
                principalTable: "TeamLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
