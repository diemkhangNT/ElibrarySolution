using Microsoft.EntityFrameworkCore;
using QuanLyTepRiengTu.Model;

namespace QuanLyTepRiengTu.Data
{
    public class FilePrivateDbContext : DbContext
    {
        public FilePrivateDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TepRiengTu> tepRiengTus { get; set; }
    }
}
