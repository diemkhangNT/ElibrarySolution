using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;
using System;

namespace QuanLyNguoiDung.Services
{
    public class CrudHVService : ICrudHVService
    {
        public readonly UserDBContext _context;

        public CrudHVService(UserDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_HocVien(string maHV)
        {
            if (_context.HocViens == null)
            {
                return false;
            }
            var hocVien = await _context.HocViens.FindAsync(maHV);
            if (hocVien == null)
            {
                return false;
            }
            _context.HocViens.Remove(hocVien);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<HocVien>> Get_HocViens()
        {
            return await _context.HocViens.ToListAsync();
        }

        public async Task<HocVien> Get_HovVien(string maHV)
        {
            var hocVien = await _context.HocViens.FindAsync(maHV);
            return hocVien;
        }

        public async Task<HocVien> Post_HocVien(HocVien hocVien)
        {
            _context.HocViens.Add(hocVien);
            await _context.SaveChangesAsync();
            return hocVien;
        }

        public async Task<HocVien> Put_HocVien(HocVien hocVien)
        {
            _context.Entry(hocVien).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hocVien;
        }
    }
}
