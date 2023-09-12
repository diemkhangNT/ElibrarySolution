using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using QuanLyThong_Bao.Data;
using QuanLyThong_Bao.Interfaces;
using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Services
{
    public class CrudTypeAnnounce : ICrudTypeAnnounce
    {
        private readonly ThongBaoDbContext _context;

        public CrudTypeAnnounce(ThongBaoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_LoaiThongbao(string maLTB)
        {
            if (_context.loaiThongBaos == null)
            {
                return false;
            }
            var loaiThongBao = await _context.loaiThongBaos.FindAsync(maLTB);
            if (loaiThongBao == null)
            {
                return false;
            }
            _context.loaiThongBaos.Remove(loaiThongBao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoaiThongBao> Get_LoaiThongBao(string maLTB)
        {
            var loaiThongBao = await _context.loaiThongBaos.FindAsync(maLTB);
            return loaiThongBao;
        }

        public async Task<IEnumerable<LoaiThongBao>> Get_LoaiThongBaos()
        {
            return await _context.loaiThongBaos.ToListAsync();
        }
        
        public async Task<LoaiThongBao> Post_LoaiThongBao(LoaiThongBao loaiThongBao)
        {
            _context.loaiThongBaos.Add(loaiThongBao);
            await _context.SaveChangesAsync();
            return loaiThongBao;
        }

        public async Task<LoaiThongBao> Put_LoaiThongBao(LoaiThongBao loaiThongBao)
        {
            _context.Entry(loaiThongBao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return loaiThongBao;
        }
    }
}
