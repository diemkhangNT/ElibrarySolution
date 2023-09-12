using Microsoft.EntityFrameworkCore;
using QuanLyThong_Bao.Data;
using QuanLyThong_Bao.Interfaces;
using QuanLyThong_Bao.Model;
using System;

namespace QuanLyThong_Bao.Services
{
    public class CrudSendAnnounce : ICrudSendAnnounce
    {
        private readonly ThongBaoDbContext _context;

        public CrudSendAnnounce(ThongBaoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_GuiTB(int STT)
        {
            if (_context.guiThongBaos == null)
            {
                return false;
            }
            var guiThongBao = await _context.guiThongBaos.FindAsync(STT);
            if (guiThongBao == null)
            {
                return false;
            }
            _context.guiThongBaos.Remove(guiThongBao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GuiThongBao> Get_GuiTB(int STT)
        {
            var guiThongBao = await _context.guiThongBaos.FindAsync(STT);
            return guiThongBao;
        }

        public async Task<IEnumerable<GuiThongBao>> Get_GuiTBs()
        {
            return await _context.guiThongBaos.ToListAsync();
        }

        public async Task<GuiThongBao> Post_GuiTB(GuiThongBao guiThongBao)
        {
            _context.guiThongBaos.Add(guiThongBao);
            await _context.SaveChangesAsync();
            return guiThongBao;
        }

        public async Task<GuiThongBao> Put_GuiTB(GuiThongBao guiThongBao)
        {
            _context.Entry(guiThongBao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return guiThongBao;
        }
    }
}
