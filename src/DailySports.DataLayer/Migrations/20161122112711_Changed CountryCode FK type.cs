using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailySports.DataLayer.Migrations
{
    public partial class ChangedCountryCodeFKtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Teams",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CountryCode",
                table: "Teams",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CountryCode",
                table: "Players",
                nullable: false);
        }
    }
}
