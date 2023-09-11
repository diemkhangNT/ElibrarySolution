using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Services
{
    public class ExtentionSV : IExtentionSV
    {
        public void AutoPK_LoaiTB(LoaiThongBao loaiThongBao)
        {
            
throw new NotImplementedException();
        }

        public void AutoPK_ThongBao(ThongBao thongBao)
        {
            const string str = "abcdefghijklmnopqostuvwxyzABCDEFGHIJKLMNOPQOSTUVWXYZ";
            Random rnd = new Random();
            int num = rnd.Next(1000, 1000000000);
            string letter = new string(Enumerable.Repeat(str, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
            thongBao.MaTB = "#TB" + num + letter;
            thongBao.NgayTao = DateTime.Now;
            thongBao.TrangThai = "SENT";
        }
    }
}
