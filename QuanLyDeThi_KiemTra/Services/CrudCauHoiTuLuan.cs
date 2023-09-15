using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Services
{
    public class CrudCauHoiTuLuan : ICrudCauHoiTuLuan
    {
        private readonly DeThiDbContext _context;

        public CrudCauHoiTuLuan(DeThiDbContext context)
        {
            _context = context;
        }
        public bool CHTuLuanExists(string id)
        {
            return (_context.CauHoiTLs?.Any(e => e.MaCH == id)).GetValueOrDefault();
        }

        public async Task<bool> Delete_CHTuLuan(string id)
        {
            if (_context.CauHoiTLs == null)
            {
                return false;
            }
            var cH = await _context.CauHoiTLs.FindAsync(id);
            if (cH == null)
            {
                return false;
            }
            _context.CauHoiTLs.Remove(cH);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CauHoiTL> Get_CHTuLuan(string id)
        {
            var cH = await _context.CauHoiTLs.FindAsync(id);
            return cH;
        }

        public async Task<IEnumerable<CauHoiTL>> Get_CHTuLuans()
        {
            return await _context.CauHoiTLs.ToListAsync();
        }

        public async Task<CauHoiTL> Post_CHTuLuan(CauHoiTL cHTuLuan)
        {
            Random rnd = new Random();
            string maCH = "#CHTL" + rnd.Next(10, 1000000000);
            cHTuLuan.MaCH = maCH;
            _context.CauHoiTLs.Add(cHTuLuan);
            await _context.SaveChangesAsync();
            return cHTuLuan;
        }

        public async Task<CauHoiTL> Put_CHTuLuan(CauHoiTL cHTuLuan)
        {
            _context.Entry(cHTuLuan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cHTuLuan;
        }
    }
}
