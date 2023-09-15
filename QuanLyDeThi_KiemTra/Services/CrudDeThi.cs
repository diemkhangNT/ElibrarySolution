using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Services
{
    public class CrudDeThi : ICrudDeThi
    {
        private readonly DeThiDbContext _context;
        private readonly IFileExtention _fileExtention;

        public CrudDeThi(DeThiDbContext context, IFileExtention fileExtention)
        {
            _context = context;
            _fileExtention = fileExtention;
        }

        public async Task<bool> Delete_DeThi(string id)
        {
            if (_context.DeThis == null)
            {
                return false;
            }
            var deThi = await _context.DeThis.FindAsync(id);
            if (deThi == null)
            {
                return false;
            }
            if(deThi.FileDeThi != "")
                _fileExtention.DeleteFile(deThi.FileDeThi, "DeThi");
            _context.DeThis.Remove(deThi);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool DeThiExists(string id)
        {
            return (_context.DeThis?.Any(e => e.MaDT == id)).GetValueOrDefault();
        }

        public async Task<DeThi> Get_DeThi(string id)
        {
            var deThi = await _context.DeThis.FindAsync(id);
            return deThi;
        }

        public async Task<IEnumerable<DeThi>> Get_DeThis()
        {
            return await _context.DeThis.ToListAsync();
        }

        public async Task<DeThi> Post_DeThi(DeThi deThi)
        {
            Random rnd = new Random();
            string maDT = "#DT" + rnd.Next(10, 1000000000);
            deThi.MaDT = maDT;
            if (deThi.SLCauHoi == null)
                deThi.SLCauHoi = 0;
            deThi.NgayTao = DateTime.Now;
            deThi.NguoiTao = "Admin";
            deThi.NgayPD = null;
            deThi.TinhTrangPD = false;

            _context.DeThis.Add(deThi);
            await _context.SaveChangesAsync();
            return deThi;
        }

        public async Task<DeThi> Put_DeThi(DeThi deThi)
        {
            DeThi existDT = await _context.DeThis.Where(id => id.MaDT == deThi.MaDT).FirstOrDefaultAsync();
            deThi.NgayTao = existDT.NgayTao;
            deThi.NguoiTao = existDT.NguoiTao;
            deThi.TinhTrangPD = existDT.TinhTrangPD;
            deThi.NgayPD = existDT.NgayPD;
            deThi.FileDeThi = existDT.FileDeThi;
            _context.DeThis.Remove(existDT);
            _context.Entry(deThi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return deThi;
        }

        public async Task<DeThi> XetDuyet_DeThi(string id)
        {
            DeThi deThi = await _context.DeThis.Where(s => s.MaDT == id).FirstOrDefaultAsync();
            deThi.NgayPD = DateTime.Now;
            deThi.TinhTrangPD = true;
            _context.Entry(deThi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return deThi;
        }
    }
}
