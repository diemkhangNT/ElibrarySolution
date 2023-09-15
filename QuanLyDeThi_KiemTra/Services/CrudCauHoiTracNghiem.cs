using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Services
{
    public class CrudCauHoiTracNghiem : ICrudCauHoiTracNghiem
    {
        private readonly DeThiDbContext _context;

        public CrudCauHoiTracNghiem(DeThiDbContext context)
        {
            _context = context;
        }

        public bool CHTracNghiemExists(string id)
        {
            return (_context.ChTracNghiems?.Any(e => e.MaCHTrN == id)).GetValueOrDefault();
        }

        public async Task<bool> Delete_CHTracNghiem(string id)
        {
            if (_context.ChTracNghiems == null)
            {
                return false;
            }
            var cH = await _context.ChTracNghiems.FindAsync(id);
            if (cH == null)
            {
                return false;
            }
            _context.ChTracNghiems.Remove(cH);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CHTracNghiem> Get_CHTracNghiem(string id)
        {
            var cH = await _context.ChTracNghiems.FindAsync(id);
            return cH;
        }

        public async Task<IEnumerable<CHTracNghiem>> Get_CHTracNghiems()
        {
            return await _context.ChTracNghiems.ToListAsync();
        }

        public async Task<CHTracNghiem> Post_CHTracNghiem(CHTracNghiem cHTracNghiem)
        {
            Random rnd = new Random();
            string maCH = "#CHTN" + rnd.Next(10, 1000000000);
            cHTracNghiem.MaCHTrN = maCH;
            _context.ChTracNghiems.Add(cHTracNghiem);
            await _context.SaveChangesAsync();
            return cHTracNghiem;
        }

        public async Task<CHTracNghiem> Put_CHTracNghiem(CHTracNghiem cHTracNghiem)
        {
            _context.Entry(cHTracNghiem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cHTracNghiem;
        }
    }
}
