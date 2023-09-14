using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTepRiengTu.Migrations
{
    public partial class ChangeNamePK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TepRiengTu",
                table: "TepRiengTu");

            migrationBuilder.DropColumn(
                name: "MaTep",
                table: "TepRiengTu");

            migrationBuilder.AddColumn<int>(
                name: "STT",
                table: "TepRiengTu",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TepRiengTu",
                table: "TepRiengTu",
                column: "STT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TepRiengTu",
                table: "TepRiengTu");

            migrationBuilder.DropColumn(
                name: "STT",
                table: "TepRiengTu");

            migrationBuilder.AddColumn<string>(
                name: "MaTep",
                table: "TepRiengTu",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TepRiengTu",
                table: "TepRiengTu",
                column: "MaTep");
        }
    }
}
