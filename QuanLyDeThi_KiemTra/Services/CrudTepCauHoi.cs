using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Services
{
    public class CrudTepCauHoi : ICrudTepCauHoi
    {
        private readonly DeThiDbContext _context;
        private readonly IFileExtention _fileExtention;

        public CrudTepCauHoi(DeThiDbContext context, IFileExtention fileExtention)
        {
            _context = context;
            _fileExtention = fileExtention;
        }

        public async Task<bool> Delete_TepCauhoi(int id)
        {
            if (_context.tepCauhois == null)
            {
                return false;
            }
            var tepCauhoi = await _context.tepCauhois.FindAsync(id);
            if (tepCauhoi == null)
            {
                return false;
            }
            if (tepCauhoi.TenTep != "")
                _fileExtention.DeleteFile(tepCauhoi.TenTep, "Cauhoi");
            _context.tepCauhois.Remove(tepCauhoi);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TepCauhoi> Get_TepCauhoi(int id)
        {
            var tep = await _context.tepCauhois.FindAsync(id);
            return tep;
        }

        public async Task<TepCauhoi> Post_TepCauhoi(TepCauhoi tepCauhoi)
        {
            _context.tepCauhois.Add(tepCauhoi);
            await _context.SaveChangesAsync();
            return tepCauhoi;

        }
    }
}
