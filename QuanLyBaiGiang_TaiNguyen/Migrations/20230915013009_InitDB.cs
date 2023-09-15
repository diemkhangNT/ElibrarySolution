using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyBaiGiang_TaiNguyen.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChuDes",
                columns: table => new
                {
                    MaCD = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaMH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuDes", x => x.MaCD);
                });

            migrationBuilder.CreateTable(
                name: "BaiGiangs",
                columns: table => new
                {
                    MaBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KichThuoc = table.Column<double>(type: "float", nullable: false),
                    TheLoai = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TenFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGuiPD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrangPD = table.Column<bool>(type: "bit", nullable: false),
                    MaCD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaMH = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiGiangs", x => x.MaBG);
                    table.ForeignKey(
                        name: "FK_BaiGiangs_ChuDes_MaCD",
                        column: x => x.MaCD,
                        principalTable: "ChuDes",
                        principalColumn: "MaCD");
                });

            migrationBuilder.CreateTable(
                name: "TaiNguyens",
                columns: table => new
                {
                    MaTN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KichThuoc = table.Column<double>(type: "float", nullable: false),
                    TheLoai = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TenFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGuiPD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrangPD = table.Column<bool>(type: "bit", nullable: false),
                    MaBG = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaCD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaMH = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiNguyens", x => x.MaTN);
                    table.ForeignKey(
                        name: "FK_TaiNguyens_BaiGiangs_MaBG",
                        column: x => x.MaBG,
                        principalTable: "BaiGiangs",
                        principalColumn: "MaBG");
                    table.ForeignKey(
                        name: "FK_TaiNguyens_ChuDes_MaCD",
                        column: x => x.MaCD,
                        principalTable: "ChuDes",
                        principalColumn: "MaCD");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiGiangs_MaCD",
                table: "BaiGiangs",
                column: "MaCD");

            migrationBuilder.CreateIndex(
                name: "IX_TaiNguyens_MaBG",
                table: "TaiNguyens",
                column: "MaBG");

            migrationBuilder.CreateIndex(
                name: "IX_TaiNguyens_MaCD",
                table: "TaiNguyens",
                column: "MaCD");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiNguyens");

            migrationBuilder.DropTable(
                name: "BaiGiangs");

            migrationBuilder.DropTable(
                name: "ChuDes");
        }
    }
}
