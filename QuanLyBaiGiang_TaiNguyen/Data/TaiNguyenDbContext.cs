using Microsoft.EntityFrameworkCore;
using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Data
{
    public class TaiNguyenDbContext : DbContext
    {
        public TaiNguyenDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TaiNguyen> TaiNguyens { get; set; }
        public DbSet<BaiGiang> BaiGiangs { get; set; }
        public DbSet<ChuDe> ChuDes { get; set; } 
    }
}
