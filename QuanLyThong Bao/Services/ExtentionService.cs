using Microsoft.EntityFrameworkCore;
using QuanLyThong_Bao.Data;
using QuanLyThong_Bao.Interfaces;
using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Services
{
    public class ExtentionService : IExtentionService
    {
        private readonly ThongBaoDbContext _context;

        public ExtentionService(ThongBaoDbContext context)
        {
            _context = context;
        }

        public void AutoPK_LoaiTB(LoaiThongBao loaiThongBao)
        {
            Random rnd = new Random();
            const string str = "abcdefghijklmnopqostuvwxyz1234567890ABCDEFGHIJKLMNOPQOSTUVWXYZ";
            string letter = new string(Enumerable.Repeat(str, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
            loaiThongBao.MaLTB = "#LTB" + letter;
        }

        public void AutoPK_ThongBao(ThongBao thongBao)
        {
            const string str = "abcdefghijklmnopqostuvwxyzABCDEFGHIJKLMNOPQOSTUVWXYZ";
            Random rnd = new Random();
            int num = rnd.Next(1000, 1000000000);
            string letter = new string(Enumerable.Repeat(str, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
            thongBao.MaTB = "#TB" + num + letter;
            thongBao.NgayTao = DateTime.Now;
            thongBao.TrangThai = "Created";
        }

        public bool LoaiThongBaoExists(string maLTB)
        {
            return (_context.loaiThongBaos?.Any(e => e.MaLTB == maLTB)).GetValueOrDefault();
        }

        public bool IsExistNameLoaiTB(string tenLTB)
        {
            return _context.loaiThongBaos.Any(u => u.TenLTB == tenLTB);
        }

        public bool ThongBaoExists(string id)
        {
            return (_context.thongBaos?.Any(e => e.MaTB == id)).GetValueOrDefault();
        }
        public bool GuiThongBaoExists(int id)
        {
            return (_context.guiThongBaos?.Any(e => e.STT == id)).GetValueOrDefault();
        }
    }
}
