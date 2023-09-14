using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTepRiengTu.Migrations
{
    public partial class ChangeAttributeSizeFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "KichThuoc",
                table: "TepRiengTu",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KichThuoc",
                table: "TepRiengTu",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
