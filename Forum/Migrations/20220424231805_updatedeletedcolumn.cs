using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Migrations
{
    public partial class updatedeletedcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicLog_Topic_TopicId",
                table: "TopicLog");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "User",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "TopicLog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Topic",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicLog_Topic_TopicId",
                table: "TopicLog",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicLog_Topic_TopicId",
                table: "TopicLog");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Topic");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "TopicLog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicLog_Topic_TopicId",
                table: "TopicLog",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
