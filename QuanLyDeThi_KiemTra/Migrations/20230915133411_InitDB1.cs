using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDeThi_KiemTra.Migrations
{
    public partial class InitDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeThi",
                columns: table => new
                {
                    MaDT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDeThi = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    SLCauHoi = table.Column<int>(type: "int", nullable: true),
                    FileDeThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiLuong = table.Column<int>(type: "int", nullable: false),
                    HinhThuc = table.Column<bool>(type: "bit", nullable: false),
                    ThangDiem = table.Column<int>(type: "int", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrangPD = table.Column<bool>(type: "bit", nullable: false),
                    NgayPD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaMH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaBM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeThi", x => x.MaDT);
                });

            migrationBuilder.CreateTable(
                name: "CauHoi",
                columns: table => new
                {
                    MaCH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhThucTL = table.Column<bool>(type: "bit", nullable: false),
                    GioiHanSoTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDeThi = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoi", x => x.MaCH);
                    table.ForeignKey(
                        name: "FK_CauHoi_DeThi_MaDeThi",
                        column: x => x.MaDeThi,
                        principalTable: "DeThi",
                        principalColumn: "MaDT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHoiTN",
                columns: table => new
                {
                    MaCHTrN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhThucTL = table.Column<bool>(type: "bit", maxLength: 25, nullable: false),
                    DoKho = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaDeThi = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoiTN", x => x.MaCHTrN);
                    table.ForeignKey(
                        name: "FK_CauHoiTN_DeThi_MaDeThi",
                        column: x => x.MaDeThi,
                        principalTable: "DeThi",
                        principalColumn: "MaDT");
                });

            migrationBuilder.CreateTable(
                name: "CauTraLoi",
                columns: table => new
                {
                    MaCH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CauTL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileCauTL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauTraLoi", x => x.MaCH);
                    table.ForeignKey(
                        name: "FK_CauTraLoi_CauHoi_MaCH",
                        column: x => x.MaCH,
                        principalTable: "CauHoi",
                        principalColumn: "MaCH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraLoiTN",
                columns: table => new
                {
                    MaCH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoiDungA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaDapAnA = table.Column<bool>(type: "bit", nullable: false),
                    NoiDungB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaDapAnB = table.Column<bool>(type: "bit", nullable: false),
                    NoiDungC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaDapAnC = table.Column<bool>(type: "bit", nullable: false),
                    NoiDungD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaDapAnD = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraLoiTN", x => x.MaCH);
                    table.ForeignKey(
                        name: "FK_TraLoiTN_CauHoiTN_MaCH",
                        column: x => x.MaCH,
                        principalTable: "CauHoiTN",
                        principalColumn: "MaCHTrN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CauHoi_MaDeThi",
                table: "CauHoi",
                column: "MaDeThi");

            migrationBuilder.CreateIndex(
                name: "IX_CauHoiTN_MaDeThi",
                table: "CauHoiTN",
                column: "MaDeThi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauTraLoi");

            migrationBuilder.DropTable(
                name: "TraLoiTN");

            migrationBuilder.DropTable(
                name: "CauHoi");

            migrationBuilder.DropTable(
                name: "CauHoiTN");

            migrationBuilder.DropTable(
                name: "DeThi");
        }
    }
}
