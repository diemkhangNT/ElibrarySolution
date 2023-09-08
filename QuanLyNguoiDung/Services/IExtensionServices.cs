using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Services
{
    public interface IExtensionServices
    {
        public void AutoPK_GiangVien(GiangVien giangVien);
        public void AutoPK_HocVien(HocVien hocVien);
        bool ValidatePassword(string password);
    }
}
