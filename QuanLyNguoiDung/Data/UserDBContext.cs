using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions options) : base(options) { }

        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<Leadership> leaderships { get; set; }
    }
}
