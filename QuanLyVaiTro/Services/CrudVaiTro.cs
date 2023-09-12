using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Data;
using QuanLyVaiTro.Interface;
using QuanLyVaiTro.Model;
using System;

namespace QuanLyVaiTro.Services
{
    public class CrudVaiTro : ICrudVaiTroService
    {
        private readonly VaiTroDbContext _context;
        public CrudVaiTro(VaiTroDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<bool> Delete_VaiTro(string maVT)
        {
            if (_context.VaiTros == null)
            {
                return false;
            }
            var vaiTro = await _context.VaiTros.FindAsync(maVT);
            if (vaiTro == null)
            {
                return false;
            }
            _context.VaiTros.Remove(vaiTro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<VaiTro> Get_VaiTro(string maVT)
        {
            var vaiTro = await _context.VaiTros.FindAsync(maVT);
            return vaiTro;
        }

        public async Task<IEnumerable<VaiTro>> Get_VaiTros()
        {
            return await _context.VaiTros.ToListAsync();
        }

        public async Task<VaiTro> Post_VaiTro(VaiTro vaiTro)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            vaiTro.MaVT = "#RO" + num;
            DateTime today = DateTime.Now;
            vaiTro.NgayCapNhat = today;
            _context.VaiTros.Add(vaiTro);
            await _context.SaveChangesAsync();
            return vaiTro;
        }

        public async Task<VaiTro> Put_VaiTro(VaiTro vaiTro)
        {
            DateTime today = DateTime.Now;
            vaiTro.NgayCapNhat = today;
            _context.Entry(vaiTro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return vaiTro;
        }

        public bool VaiTroExists(string maVT)
        {
            return (_context.VaiTros?.Any(e => e.MaVT == maVT)).GetValueOrDefault();
        }
    }
}
