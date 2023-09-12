using Microsoft.EntityFrameworkCore;
using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Interfaces
{
    public interface IExtentionService
    {
        public void AutoPK_ThongBao(ThongBao thongBao);
        public void AutoPK_LoaiTB(LoaiThongBao loaiThongBao);
        public bool LoaiThongBaoExists(string maLTB);
        bool IsExistNameLoaiTB(string tenLTB);
        bool ThongBaoExists(string id);
        bool GuiThongBaoExists(int id);
    }
}
