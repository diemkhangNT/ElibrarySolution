using QuanLyNguoiDung.Model;
using System.Text.RegularExpressions;

namespace QuanLyNguoiDung.Services
{
    public class ExtensionServices : IExtensionServices
    {
        public void AutoPK_GiangVien(GiangVien giangVien)
        {
            Random rnd = new Random();
            string num = rnd.Next(100, 1000000000).ToString();
            giangVien.MaGV = "#GV" + num;
            giangVien.NgayHopTac = DateTime.Now;
            giangVien.TrangThaiHD = true;
        }

        public void AutoPK_HocVien(HocVien hocVien)
        {
            Random rnd = new Random();
            string num = rnd.Next(100, 1000000000).ToString();
            hocVien.MaHV = "#HV" + num;
            hocVien.NgayTao = DateTime.Now;
        }

        public bool ValidatePassword(string password)
        {
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{8,}$";
            bool isValid = Regex.IsMatch(password, passwordRegex);
            return isValid;
        }
    }
}
