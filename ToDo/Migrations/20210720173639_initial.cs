using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    listOfPerformers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    planedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    realTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    masterTaskid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.id);
                    table.ForeignKey(
                        name: "FK_Task_Task_masterTaskid",
                        column: x => x.masterTaskid,
                        principalTable: "Task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_masterTaskid",
                table: "Task",
                column: "masterTaskid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
