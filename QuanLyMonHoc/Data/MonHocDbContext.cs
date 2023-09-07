using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Data
{
    public class MonHocDbContext : DbContext
    {
        public MonHocDbContext(DbContextOptions<MonHocDbContext> options) : base(options) { }

        public virtual DbSet<MonHoc> MonHocs { get; set; }

        public DbSet<QuanLyMonHoc.Model.HoiDap>? HoiDap { get; set; }

        public DbSet<QuanLyMonHoc.Model.LopHoc>? LopHoc { get; set; }

        public DbSet<QuanLyMonHoc.Model.NienKhoa>? NienKhoa { get; set; }

        public DbSet<QuanLyMonHoc.Model.TraLoi>? TraLoi { get; set; }
    }
}
