using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Interface
{
    public interface IExtension
    {
        public void AutoPK_MonHoc(MonHoc monHoc);
        bool IsExistNameMonHoc_Post(string name);
        bool IsExistNameMonHoc_Put(MonHoc monHoc);
        public void AutoPK_LopHoc(LopHoc lopHoc);
        public void AutoPK_NienKhoa(NienKhoa nienKhoa);
        bool IsCheckTime(int tgBD, int tgKT);
        bool IsCheckTime_put(int tgBD, int tgKT, string maNK);
        public void AutoPK_HoiDap(HoiDap hoiDap);
        public void AutoPK_TraLoi(TraLoi traLoi);
        bool HoiDapExists(string id);
        bool LopHocExists(string id);
        bool MonHocExists(string id);
        bool NienKhoaExists(string id);
        bool TraLoiExists(string id);
    }
}
