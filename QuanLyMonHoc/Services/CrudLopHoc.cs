using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Interface;
using QuanLyMonHoc.Model;
using System;

namespace QuanLyMonHoc.Services
{
    public class CrudLopHoc : ICrudLopHoc
    {
        private readonly MonHocDbContext _context;

        public CrudLopHoc(MonHocDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_LopHoc(string maLH)
        {
            if (_context.LopHocs == null)
            {
                return false;
            }
            var lopHoc = await _context.LopHocs.FindAsync(maLH);
            if (lopHoc == null)
            {
                return false;
            }
            _context.LopHocs.Remove(lopHoc);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LopHoc> Get_LopHoc(string maLH)
        {
            var lop = await _context.LopHocs.FindAsync(maLH);
            return lop;
        }

        public async Task<IEnumerable<LopHoc>> Get_LopHocs()
        {
            return await _context.LopHocs.ToListAsync();
        }

        public async Task<LopHoc> Post_LopHoc(LopHoc lopHoc)
        {
            _context.LopHocs.Add(lopHoc);
            await _context.SaveChangesAsync();
            return lopHoc;
        }

        public async Task<LopHoc> Put_LopHoc(LopHoc lopHoc)
        {
            _context.Entry(lopHoc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return lopHoc;
        }
    }
}
