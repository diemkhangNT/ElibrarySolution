using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThong_Bao.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiThongbao",
                columns: table => new
                {
                    MaLTB = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLTB = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThongbao", x => x.MaLTB);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    MaTB = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLTB = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.MaTB);
                    table.ForeignKey(
                        name: "FK_ThongBao_LoaiThongbao_MaLTB",
                        column: x => x.MaLTB,
                        principalTable: "LoaiThongbao",
                        principalColumn: "MaLTB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuiThongBao",
                columns: table => new
                {
                    STT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiGui = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaTB = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiThongBao", x => x.STT);
                    table.ForeignKey(
                        name: "FK_GuiThongBao_ThongBao_MaTB",
                        column: x => x.MaTB,
                        principalTable: "ThongBao",
                        principalColumn: "MaTB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuiThongBao_MaTB",
                table: "GuiThongBao",
                column: "MaTB");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_MaLTB",
                table: "ThongBao",
                column: "MaLTB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuiThongBao");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "LoaiThongbao");
        }
    }
}
