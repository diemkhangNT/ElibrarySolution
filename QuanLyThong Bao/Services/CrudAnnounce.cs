using Microsoft.EntityFrameworkCore;
using QuanLyThong_Bao.Data;
using QuanLyThong_Bao.Interfaces;
using QuanLyThong_Bao.Model;
using System;

namespace QuanLyThong_Bao.Services
{
    public class CrudAnnounce : ICrudAnnounce
    {
        private readonly ThongBaoDbContext _context;

        public CrudAnnounce(ThongBaoDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete_ThongBao(string maTB)
        {
            if (_context.thongBaos == null)
            {
                return false;
            }
            var thongBao = await _context.thongBaos.FindAsync(maTB);
            if (thongBao == null)
            {
                return false;
            }
            _context.thongBaos.Remove(thongBao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ThongBao> Get_ThongBao(string maTB)
        {
            var thongBao = await _context.thongBaos.FindAsync(maTB);
            return thongBao;
        }

        public async Task<IEnumerable<ThongBao>> Get_ThongBaos()
        {
            return await _context.thongBaos.ToListAsync();
        }

        public async Task<ThongBao> Post_ThongBao(ThongBao thongBao)
        {
            _context.thongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
            return thongBao;
        }

        public async Task<ThongBao> Put_ThongBao(ThongBao thongBao)
        {
            _context.Entry(thongBao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return thongBao;
        }
    }
}
