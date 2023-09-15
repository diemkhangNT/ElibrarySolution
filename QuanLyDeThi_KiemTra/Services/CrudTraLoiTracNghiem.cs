using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Services
{
    public class CrudTraLoiTracNghiem : ICrudTraLoiTracNghiem
    {
        private readonly DeThiDbContext _context;

        public CrudTraLoiTracNghiem(DeThiDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete_TLTracNghiem(string id)
        {
            if (_context.TLTracNghiems == null)
            {
                return false;
            }
            var tL = await _context.TLTracNghiems.FindAsync(id);
            if (tL == null)
            {
                return false;
            }
            _context.TLTracNghiems.Remove(tL);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TLTracNghiem> Get_TLTracNghiem(string id)
        {
            var tL = await _context.TLTracNghiems.FindAsync(id);
            return tL;
        }

        public async Task<IEnumerable<TLTracNghiem>> Get_TLTracNghiems()
        {
            return await _context.TLTracNghiems.ToListAsync();
        }

        public async Task<TLTracNghiem> Post_TLTracNghiem([FromForm] TLTracNghiem tLTracNghiem)
        {
            _context.TLTracNghiems.Add(tLTracNghiem);
            await _context.SaveChangesAsync();
            return tLTracNghiem;
        }

        public async Task<TLTracNghiem> Put_TLTracNghiem([FromForm] TLTracNghiem tLTracNghiem)
        {
            _context.Entry(tLTracNghiem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tLTracNghiem;
        }

        public bool TLTracNghiemExists(string id)
        {
            return (_context.TLTracNghiems?.Any(e => e.MaCH == id)).GetValueOrDefault();
        }
    }
}
