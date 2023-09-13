using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Interface;
using QuanLyMonHoc.Model;
using System;

namespace QuanLyMonHoc.Services
{
    public class CrudTraLoi : ICrudTraLoi
    {
        private readonly MonHocDbContext _context;

        public CrudTraLoi(MonHocDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete_TraLoi(string maTL)
        {
            if (_context.TraLois == null)
            {
                return false;
            }
            var traLoi = await _context.TraLois.FindAsync(maTL);
            if (traLoi == null)
            {
                return false;
            }
            _context.TraLois.Remove(traLoi);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TraLoi> Get_TraLoi(string maTL)
        {
            var traLoi = await _context.TraLois.FindAsync(maTL);
            return traLoi;
        }

        public async Task<IEnumerable<TraLoi>> Get_TraLois()
        {
            return await _context.TraLois.ToListAsync();
        }

        public async Task<TraLoi> Post_TraLoi(TraLoi traLoi)
        {
            _context.TraLois.Add(traLoi);
            await _context.SaveChangesAsync();
            return traLoi;
        }

        public async Task<TraLoi> Put_TraLoi(TraLoi traLoi)
        {
            traLoi.ThoiGian = DateTime.Now;
            _context.Entry(traLoi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return traLoi;
        }
    }
}
