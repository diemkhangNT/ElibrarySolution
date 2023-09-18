using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDeThi_KiemTra.Migrations
{
    public partial class AddTepCauHoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TepCauHoi",
                columns: table => new
                {
                    STT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTep = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TepCauHoi", x => x.STT);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TepCauHoi");
        }
    }
}
