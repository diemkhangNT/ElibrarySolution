using Microsoft.EntityFrameworkCore;
using QuanLyBaiGiang_TaiNguyen.Data;
using QuanLyBaiGiang_TaiNguyen.Interface;
using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Services
{
    public class CrudChuDe : ICrudChuDe
    {
        private readonly TaiNguyenDbContext _context;

        public CrudChuDe(TaiNguyenDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_ChuDe(string maCD)
        {
            if (_context.ChuDes == null)
            {
                return false;
            }
            var chuDe = await _context.ChuDes.FindAsync(maCD);
            if (chuDe == null)
            {
                return false;
            }
            _context.ChuDes.Remove(chuDe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ChuDe> Get_ChuDe(string maCD)
        {
            var chuDe = await _context.ChuDes.FindAsync(maCD);
            return chuDe;
        }

        public async Task<IEnumerable<ChuDe>> Get_ChuDes()
        {
            return await _context.ChuDes.ToListAsync();
        }

        public async Task<ChuDe> Post_ChuDe(ChuDe chuDe)
        {
            Random rnd = new Random();
            string maCD = "#CD" + rnd.Next(10,1000000000);
            chuDe.MaCD = maCD;
            _context.ChuDes.Add(chuDe);
            await _context.SaveChangesAsync();
            return chuDe;
        }

        public async Task<ChuDe> Put_ChuDe(ChuDe chuDe)
        {
            _context.Entry(chuDe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return chuDe;
        }

        public bool ChuDeExists(string id)
        {
            return (_context.ChuDes?.Any(e => e.MaCD == id)).GetValueOrDefault();
        }
    }
}
