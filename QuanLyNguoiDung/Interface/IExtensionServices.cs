using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Interface
{
    public interface IExtensionServices
    {
        public void AutoPK_GiangVien(GiangVien giangVien);
        public void AutoPK_HocVien(HocVien hocVien);
        public void AutoPK_Leader(Leadership leadership);
        bool ValidatePassword(string password);
        bool IsEmailGVUnique(string emailGV);
        bool IsEmailHVUnique(string emailHV);
        bool IsEmailLDUnique(string emailLD);
        bool IsNumberPhone(string sdt);
        bool IsUserNameGVUnique(string username);
        bool IsUserNameHVUnique(string username);
        bool IsUserNameLDUnique(string username);
        bool HocVienExists(string id);
        bool GiangVienExists(string id);
        bool LeadershipExists(string id);
        public void UploadImageGV(GiangVien giangVien, IFormFile imageFile);
        public void UploadImageHV(HocVien hocVien, IFormFile imageFile);
        public void UploadImageLD(Leadership leadership, IFormFile imageFile);
        public bool GetImageById(string fileName);
    }
}
