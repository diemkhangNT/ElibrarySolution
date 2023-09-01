using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyVaiTro.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    MaVT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenVT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.MaVT);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyen",
                columns: table => new
                {
                    MaPQ = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenChucNang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Xem = table.Column<bool>(type: "bit", nullable: false),
                    Xoa = table.Column<bool>(type: "bit", nullable: false),
                    Sua = table.Column<bool>(type: "bit", nullable: false),
                    ThemMoi = table.Column<bool>(type: "bit", nullable: false),
                    PheDuyet = table.Column<bool>(type: "bit", nullable: false),
                    MaVT = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyen", x => x.MaPQ);
                    table.ForeignKey(
                        name: "FK_PhanQuyen_VaiTro_MaVT",
                        column: x => x.MaVT,
                        principalTable: "VaiTro",
                        principalColumn: "MaVT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_MaVT",
                table: "PhanQuyen",
                column: "MaVT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhanQuyen");

            migrationBuilder.DropTable(
                name: "VaiTro");
        }
    }
}
