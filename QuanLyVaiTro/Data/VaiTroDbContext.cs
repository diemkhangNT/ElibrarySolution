using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Data
{
    public class VaiTroDbContext: DbContext
    {
        public VaiTroDbContext(DbContextOptions<VaiTroDbContext> options) : base(options) { }

        public virtual DbSet<VaiTro> VaiTros { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

        //public override int SaveChanges()
        //{
        //    Random rnd = new Random();
        //    //const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
        //    foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        //    {
        //        if (entry.Entity is VaiTro vaiTro)
        //        {
        //            //string num = new string(Enumerable.Repeat(chars, 9).Select(s => s[rnd.Next(s.Length)]).ToArray());

        //            string num = rnd.Next(1000, 10000000).ToString();
        //            vaiTro.MaVT = "#RO" + num;
        //            if (vaiTro.MoTa == "string")
        //                vaiTro.MoTa = null;
        //            DateTime today = DateTime.Now;
        //            vaiTro.NgayCapNhat = today;
        //        }
        //    }
        //    return base.SaveChanges();
        //}
    }
}
