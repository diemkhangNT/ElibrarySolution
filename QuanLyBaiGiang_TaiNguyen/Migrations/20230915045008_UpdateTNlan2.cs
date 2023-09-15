using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyBaiGiang_TaiNguyen.Migrations
{
    public partial class UpdateTNlan2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DonViTinh",
                table: "TaiNguyens",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DonViTinh",
                table: "BaiGiangs",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonViTinh",
                table: "TaiNguyens");

            migrationBuilder.DropColumn(
                name: "DonViTinh",
                table: "BaiGiangs");
        }
    }
}
