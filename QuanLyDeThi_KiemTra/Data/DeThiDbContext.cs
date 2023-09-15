using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Data
{
    public class DeThiDbContext : DbContext
    {
        public DeThiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DeThi> DeThis { get; set; }
        public DbSet<CauHoiTL> CauHoiTLs { get; set; }
        public DbSet<CHTracNghiem> ChTracNghiems { get; set; }
        public DbSet<TraLoiTL> CauTraLoiTLs { get;set; }
        public DbSet<TLTracNghiem> TLTracNghiems { get;set; }
    }

}
