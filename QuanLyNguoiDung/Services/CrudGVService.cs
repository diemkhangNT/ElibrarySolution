using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Services
{
    public class CrudGVService : ICrudGVService
    {
        private readonly UserDBContext _context;

        public CrudGVService(UserDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_GiangVien(string maGV)
        {
            if (_context.GiangViens == null)
            {
                return false;
            }
            var giangVien = await _context.GiangViens.FindAsync(maGV);
            if (giangVien == null)
            {
                return false;
            }
            _context.GiangViens.Remove(giangVien);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GiangVien> Get_GiangVien(string maGV)
        {
            var giangVien = await _context.GiangViens.FindAsync(maGV);
            return giangVien;
        }

        public async Task<IEnumerable<GiangVien>> Get_GiangViens()
        {
            return await _context.GiangViens.ToListAsync();
        }

        public async Task<GiangVien> Post_GiangVien(GiangVien giangVien)
        {
            _context.GiangViens.Add(giangVien);
            await _context.SaveChangesAsync();
            return giangVien;
        }

        public async Task<GiangVien> Put_GiangVien(GiangVien giangVien)
        {
            _context.Entry(giangVien).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return giangVien;
        }
    }
}
