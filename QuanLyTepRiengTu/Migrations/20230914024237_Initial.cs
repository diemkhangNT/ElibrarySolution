using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTepRiengTu.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TepRiengTu",
                columns: table => new
                {
                    MaTep = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenTep = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KichThuoc = table.Column<int>(type: "int", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TheLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TepRiengTu", x => x.MaTep);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TepRiengTu");
        }
    }
}
