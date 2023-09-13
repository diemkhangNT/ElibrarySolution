using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Interface;
using QuanLyMonHoc.Model;
using System;

namespace QuanLyMonHoc.Services
{
    public class CrudMonHoc : ICrudMonHoc
    {
        private readonly MonHocDbContext _context;

        public CrudMonHoc(MonHocDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete_MonHoc(string maMH)
        {
            if (_context.MonHocs == null)
            {
                return false;
            }
            var monHoc = await _context.MonHocs.FindAsync(maMH);
            if (monHoc == null)
            {
                return false;
            }
            _context.MonHocs.Remove(monHoc);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MonHoc> Get_MonHoc(string maMH)
        {
            var monHoc = await _context.MonHocs.FindAsync(maMH);
            return monHoc;
        }

        public async Task<IEnumerable<MonHoc>> Get_MonHocs()
        {
            return await _context.MonHocs.ToListAsync();
        }

        public async Task<MonHoc> Post_MonHoc(MonHoc monHoc)
        {
            monHoc.NgayGuiPheDuyet = DateTime.Now;
            _context.MonHocs.Add(monHoc);
            await _context.SaveChangesAsync();
            return monHoc;
        }

        public async Task<MonHoc> Put_MonHoc(MonHoc monHoc)
        {
            monHoc.NgayGuiPheDuyet = DateTime.Now;
            _context.Entry(monHoc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return monHoc;
        }
    }
}
