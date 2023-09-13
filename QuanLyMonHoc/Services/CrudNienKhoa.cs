using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Interface;
using QuanLyMonHoc.Model;
using System;

namespace QuanLyMonHoc.Services
{
    public class CrudNienKhoa : ICrudNienKhoa
    {
        private readonly MonHocDbContext _context;

        public CrudNienKhoa(MonHocDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete_NienKhoa(string maNK)
        {
            if (_context.NienKhoas == null)
            {
                return false;
            }
            var nienKhoa = await _context.NienKhoas.FindAsync(maNK);
            if (nienKhoa == null)
            {
                return false;
            }
            _context.NienKhoas.Remove(nienKhoa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<NienKhoa> Get_NienKhoa(string maNK)
        {
            var nienKhoa = await _context.NienKhoas.FindAsync(maNK);
            return nienKhoa;
        }

        public async Task<IEnumerable<NienKhoa>> Get_NienKhoas()
        {
            return await _context.NienKhoas.ToListAsync();
        }

        public async Task<NienKhoa> Post_NienKhoa(NienKhoa nienKhoa)
        {
            _context.NienKhoas.Add(nienKhoa);
            await _context.SaveChangesAsync();
            return nienKhoa;
        }

        public async Task<NienKhoa> Put_NienKhoa(NienKhoa nienKhoa)
        {
            _context.Entry(nienKhoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return nienKhoa;
        }
    }
}
