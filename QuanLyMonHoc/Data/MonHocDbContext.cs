using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Data
{
    public class MonHocDbContext : DbContext
    {
        public MonHocDbContext(DbContextOptions<MonHocDbContext> options) : base(options) { }

        public virtual DbSet<MonHoc> MonHocs { get; set; }

        public DbSet<QuanLyMonHoc.Model.HoiDap>? HoiDaps { get; set; }

        public DbSet<QuanLyMonHoc.Model.LopHoc>? LopHocs { get; set; }

        public DbSet<QuanLyMonHoc.Model.NienKhoa>? NienKhoas { get; set; }

        public DbSet<QuanLyMonHoc.Model.TraLoi>? TraLois { get; set; }
    }
}
