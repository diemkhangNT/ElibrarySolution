using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Data;
using QuanLyVaiTro.Interface;
using QuanLyVaiTro.Model;
using System;

namespace QuanLyVaiTro.Services
{
    public class CrudPhanQuyen : ICrudPhanQuyenService
    {
        private readonly VaiTroDbContext _context;

        public CrudPhanQuyen(VaiTroDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_PhanQuyen(string maPQ)
        {
            if (_context.PhanQuyens == null)
            {
                return false;
            }
            var phanQuyen = await _context.PhanQuyens.FindAsync(maPQ);
            if (phanQuyen == null)
            {
                return false;
            }
            _context.PhanQuyens.Remove(phanQuyen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PhanQuyen> Get_PhanQuyen(string maPQ)
        {
            var pq = await _context.PhanQuyens.FindAsync(maPQ);
            return pq;
        }

        public async Task<IEnumerable<PhanQuyen>> Get_PhanQuyens()
        {
            return await _context.PhanQuyens.ToListAsync();
        }

        public bool PhanQuyenExists(string maPQ)
        {
            return (_context.PhanQuyens?.Any(e => e.MaPQ == maPQ)).GetValueOrDefault();
        }

        public async Task<PhanQuyen> Post_PhanQuyen(PhanQuyen phanQuyen)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            phanQuyen.MaPQ = "#QR" + num;
            _context.PhanQuyens.Add(phanQuyen);
            _context.PhanQuyens.Add(phanQuyen);
            await _context.SaveChangesAsync();
            return phanQuyen;
        }

        public async Task<PhanQuyen> Put_PhanQuyen(PhanQuyen phanQuyen)
        {
            _context.Entry(phanQuyen).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return phanQuyen;
        }
    }
}
