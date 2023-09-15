using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDeThi_KiemTra.Migrations
{
    public partial class InitDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CauHoi_DeThi_MaDeThi",
                table: "CauHoi");

            migrationBuilder.DropForeignKey(
                name: "FK_CauTraLoi_CauHoi_MaCH",
                table: "CauTraLoi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CauTraLoi",
                table: "CauTraLoi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CauHoi",
                table: "CauHoi");

            migrationBuilder.RenameTable(
                name: "CauTraLoi",
                newName: "CauTraLoiTL");

            migrationBuilder.RenameTable(
                name: "CauHoi",
                newName: "CauHoiTL");

            migrationBuilder.RenameIndex(
                name: "IX_CauHoi_MaDeThi",
                table: "CauHoiTL",
                newName: "IX_CauHoiTL_MaDeThi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CauTraLoiTL",
                table: "CauTraLoiTL",
                column: "MaCH");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CauHoiTL",
                table: "CauHoiTL",
                column: "MaCH");

            migrationBuilder.AddForeignKey(
                name: "FK_CauHoiTL_DeThi_MaDeThi",
                table: "CauHoiTL",
                column: "MaDeThi",
                principalTable: "DeThi",
                principalColumn: "MaDT",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CauTraLoiTL_CauHoiTL_MaCH",
                table: "CauTraLoiTL",
                column: "MaCH",
                principalTable: "CauHoiTL",
                principalColumn: "MaCH",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CauHoiTL_DeThi_MaDeThi",
                table: "CauHoiTL");

            migrationBuilder.DropForeignKey(
                name: "FK_CauTraLoiTL_CauHoiTL_MaCH",
                table: "CauTraLoiTL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CauTraLoiTL",
                table: "CauTraLoiTL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CauHoiTL",
                table: "CauHoiTL");

            migrationBuilder.RenameTable(
                name: "CauTraLoiTL",
                newName: "CauTraLoi");

            migrationBuilder.RenameTable(
                name: "CauHoiTL",
                newName: "CauHoi");

            migrationBuilder.RenameIndex(
                name: "IX_CauHoiTL_MaDeThi",
                table: "CauHoi",
                newName: "IX_CauHoi_MaDeThi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CauTraLoi",
                table: "CauTraLoi",
                column: "MaCH");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CauHoi",
                table: "CauHoi",
                column: "MaCH");

            migrationBuilder.AddForeignKey(
                name: "FK_CauHoi_DeThi_MaDeThi",
                table: "CauHoi",
                column: "MaDeThi",
                principalTable: "DeThi",
                principalColumn: "MaDT",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CauTraLoi_CauHoi_MaCH",
                table: "CauTraLoi",
                column: "MaCH",
                principalTable: "CauHoi",
                principalColumn: "MaCH",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
