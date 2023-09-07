using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMonHoc.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaGV",
                table: "MonHoc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HoiDap",
                columns: table => new
                {
                    MaCauHoi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaMH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaHV = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoiDap", x => x.MaCauHoi);
                    table.ForeignKey(
                        name: "FK_HoiDap_MonHoc_MaMH",
                        column: x => x.MaMH,
                        principalTable: "MonHoc",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NienKhoa",
                columns: table => new
                {
                    MaNK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TGBatDau = table.Column<int>(type: "int", nullable: false),
                    TGKetThuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NienKhoa", x => x.MaNK);
                });

            migrationBuilder.CreateTable(
                name: "HoiDap_TL",
                columns: table => new
                {
                    MaCauTL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaCauHoi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaTacGia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoiDap_TL", x => x.MaCauTL);
                    table.ForeignKey(
                        name: "FK_HoiDap_TL_HoiDap_MaCauHoi",
                        column: x => x.MaCauHoi,
                        principalTable: "HoiDap",
                        principalColumn: "MaCauHoi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    MaLop = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLop = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SiSo = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaMH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaNK = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.MaLop);
                    table.ForeignKey(
                        name: "FK_LopHoc_MonHoc_MaMH",
                        column: x => x.MaMH,
                        principalTable: "MonHoc",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHoc_NienKhoa_MaNK",
                        column: x => x.MaNK,
                        principalTable: "NienKhoa",
                        principalColumn: "MaNK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoiDap_MaMH",
                table: "HoiDap",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_HoiDap_TL_MaCauHoi",
                table: "HoiDap_TL",
                column: "MaCauHoi");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_MaMH",
                table: "LopHoc",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_MaNK",
                table: "LopHoc",
                column: "MaNK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoiDap_TL");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropTable(
                name: "HoiDap");

            migrationBuilder.DropTable(
                name: "NienKhoa");

            migrationBuilder.DropColumn(
                name: "MaGV",
                table: "MonHoc");
        }
    }
}
