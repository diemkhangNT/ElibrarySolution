using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Data
{
    public class MonHocDbContext : DbContext
    {
        public MonHocDbContext(DbContextOptions<MonHocDbContext> options) : base(options) { }

        public virtual DbSet<MonHoc> MonHocs { get; set; }

        public override int SaveChanges()
        {
            Random rnd = new Random();
            //const string chars = "abcdefghijklmnopqrstuvwsyz0123456789";
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity is MonHoc monHoc)
                {
                    //string num = new string(Enumerable.Repeat(chars, 9).Select(s => s[rnd.Next(s.Length)]).ToArray());

                    string num = rnd.Next(1000, 10000000).ToString();
                    monHoc.MaMH = "#DLK" + num;
                    if (monHoc.MoTa == "string")
                        monHoc.MoTa = null;
                }
            }
            return base.SaveChanges();
        }
    }
}
