using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Data
{
    public class VaiTroDbContext: DbContext
    {
        public VaiTroDbContext(DbContextOptions<VaiTroDbContext> options) : base(options) { }

        public virtual DbSet<VaiTro> VaiTros { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }


    }
}
