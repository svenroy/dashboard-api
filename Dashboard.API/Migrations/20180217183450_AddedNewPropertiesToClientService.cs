using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.API.Migrations
{
    public partial class AddedNewPropertiesToClientService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClientServices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ClientServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ClientServices",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ClientServices");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ClientServices");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClientServices",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
