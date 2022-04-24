using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogonLogs_User_UserId",
                table: "LogonLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogonLogs",
                table: "LogonLogs");

            migrationBuilder.RenameTable(
                name: "LogonLogs",
                newName: "LogonLog");

            migrationBuilder.RenameIndex(
                name: "IX_LogonLogs_UserId",
                table: "LogonLog",
                newName: "IX_LogonLog_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogonLog",
                table: "LogonLog",
                column: "LogonLogId");

            migrationBuilder.CreateTable(
                name: "Logon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logon", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LogonLog_User_UserId",
                table: "LogonLog",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogonLog_User_UserId",
                table: "LogonLog");

            migrationBuilder.DropTable(
                name: "Logon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogonLog",
                table: "LogonLog");

            migrationBuilder.RenameTable(
                name: "LogonLog",
                newName: "LogonLogs");

            migrationBuilder.RenameIndex(
                name: "IX_LogonLog_UserId",
                table: "LogonLogs",
                newName: "IX_LogonLogs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogonLogs",
                table: "LogonLogs",
                column: "LogonLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogonLogs_User_UserId",
                table: "LogonLogs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
