﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace CIT.HelpDesk.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class usermigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Users",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Users", x => x.Id);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Users");
        }
    }
}