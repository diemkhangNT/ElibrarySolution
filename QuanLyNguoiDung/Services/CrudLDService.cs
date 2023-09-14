using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Services
{
    public class CrudLDService : ICrudLDService
    {
        private readonly UserDBContext _context;

        public CrudLDService(UserDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_Leadership(string maLD)
        {
            if (_context.leaderships == null)
            {
                return false;
            }
            var lds = await _context.leaderships.FindAsync(maLD);
            if (lds == null)
            {
                return false;
            }
            _context.leaderships.Remove(lds);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Leadership> Get_Leadership(string maLD)
        {
            var leadership = await _context.leaderships.FindAsync(maLD);
            return leadership;
        }

        public async Task<IEnumerable<Leadership>> Get_Leaderships()
        {
            return await _context.leaderships.ToListAsync();
        }

        public async Task<Leadership> Post_Leadership(Leadership leadership)
        {
            _context.leaderships.Add(leadership);
            await _context.SaveChangesAsync();
            return leadership;
        }

        public async Task<Leadership> Put_Leadership(Leadership leadership)
        {
            _context.Entry(leadership).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return leadership;
        }
    }
}
