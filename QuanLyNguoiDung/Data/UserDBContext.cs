using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions options) : base(options)
        {
        }

        //#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //        public UserDBContext(DbContextOptions options) : base(options)
        //#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //        {
        //        }

        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<Leadership> leaderships { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
    }
}
