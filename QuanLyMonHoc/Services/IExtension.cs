using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Services
{
    public interface IExtension
    {
        public void AutoPK_MonHoc(MonHoc monHoc);
        bool IsExistNameMonHoc_Post(string name);
        bool IsExistNameMonHoc_Put(MonHoc monHoc);
        public void AutoPK_LopHoc(LopHoc lopHoc);
        public void AutoPK_NienKhoa(NienKhoa nienKhoa);
        bool IsCheckTime(int tgBD, int tgKT);
        public void AutoPK_HoiDap(HoiDap hoiDap);
        public void AutoPK_TraLoi(TraLoi traLoi);

    }
}
