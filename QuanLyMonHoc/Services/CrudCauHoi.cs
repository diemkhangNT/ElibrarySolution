using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Interface;
using QuanLyMonHoc.Model;
using System;

namespace QuanLyMonHoc.Services
{
    public class CrudCauHoi : ICrudCauHoi
    {
        private readonly MonHocDbContext _context;

        public CrudCauHoi(MonHocDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_HoiDap(string maCH)
        {
            if (_context.HoiDaps == null)
            {
                return false;
            }
            var hoiDap = await _context.HoiDaps.FindAsync(maCH);
            if (hoiDap == null)
            {
                return false;
            }
            _context.HoiDaps.Remove(hoiDap);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<HoiDap> Get_HoiDap(string maCH)
        {
            var hoiDap = await _context.HoiDaps.FindAsync(maCH);
            return hoiDap;
        }

        public async Task<IEnumerable<HoiDap>> Get_HoiDaps()
        {
            return await _context.HoiDaps.ToListAsync();
        }

        public async Task<HoiDap> Post_HoiDap(HoiDap hoiDap)
        {
            hoiDap.ThoiGian = DateTime.Now;
            _context.HoiDaps.Add(hoiDap);
            await _context.SaveChangesAsync();
            return hoiDap;
        }

        public async Task<HoiDap> Put_HoiDap(HoiDap hoiDap)
        {
            hoiDap.ThoiGian = DateTime.Now;
            _context.Entry(hoiDap).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hoiDap;
        }
    }
}
