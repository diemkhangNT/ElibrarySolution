using Microsoft.EntityFrameworkCore;
using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Data
{
    public class ThongBaoDbContext : DbContext
    {
        public ThongBaoDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ThongBao> thongBaos { get; set; }
        public virtual DbSet<GuiThongBao> guiThongBaos { get; set; }
        public virtual DbSet<LoaiThongBao> loaiThongBaos { get; set; }
    }
}
