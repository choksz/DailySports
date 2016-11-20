using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class GendertoPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PetOfTheWeek");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "PetOfTheWeek",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PetOfTheWeek");

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "PetOfTheWeek",
                nullable: false);
        }
    }
}
