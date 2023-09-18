using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;
using System.Linq;

namespace QuanLyDeThi_KiemTra.Services
{
    public class CrudTraLoiTuLuan : ICrudTraLoiTuLuan
    {
        private readonly DeThiDbContext _context;
        private readonly IFileExtention _fileExtention;

        public CrudTraLoiTuLuan(DeThiDbContext context, IFileExtention fileExtention)
        {
            _context = context;
            _fileExtention = fileExtention;
        }

        public bool Check_HinhThucTL(string id)
        {
            CauHoiTL cauHoiTL = _context.CauHoiTLs.Where(s=> s.MaCH == id).FirstOrDefault();
            if (cauHoiTL.HinhThucTL)
            {
                return true;
            }else return false;
        }

        public async Task<bool> Delete_TraLoiTL(string id)
        {
            if (_context.CauTraLoiTLs == null)
            {
                return false;
            }
            var tL = await _context.CauTraLoiTLs.FindAsync(id);
            if (tL == null)
            {
                return false;
            }
            if (tL.FileCauTL != "")
                _fileExtention.DeleteFile(tL.FileCauTL, "DapAnTuLuan");
            _context.CauTraLoiTLs.Remove(tL);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TraLoiTL> Get_TraLoiTL(string id)
        {
            var tL = await _context.CauTraLoiTLs.FindAsync(id);
            return tL;
        }

        public async Task<IEnumerable<TraLoiTL>> Get_TraLoiTLs()
        {
            return await _context.CauTraLoiTLs.ToListAsync();
        }

        public async Task<TraLoiTL> Post_TraLoiTL(TraLoiTL traLoiTL)
        {
            _context.CauTraLoiTLs.Add(traLoiTL);
            await _context.SaveChangesAsync();
            return traLoiTL;
        }

        public async Task<TraLoiTL> Put_TraLoiTL(TraLoiTL traLoiTL)
        {
            _context.Entry(traLoiTL).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return traLoiTL;
        }

        public bool TraLoiTLExists(string id)
        {
            return (_context.CauTraLoiTLs?.Any(e => e.MaCH == id)).GetValueOrDefault();
        }
    }
}
