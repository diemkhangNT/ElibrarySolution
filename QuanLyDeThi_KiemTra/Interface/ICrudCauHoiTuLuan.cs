using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Interface
{
    public interface ICrudCauHoiTuLuan
    {
        public Task<IEnumerable<CauHoiTL>> Get_CHTuLuans();
        public Task<CauHoiTL> Get_CHTuLuan(string id);
        public Task<CauHoiTL> Put_CHTuLuan(CauHoiTL cHTuLuan);
        public Task<CauHoiTL> Post_CHTuLuan(CauHoiTL cHTuLuan);
        public Task<bool> Delete_CHTuLuan(string id);
        bool CHTuLuanExists(string id);
    }
}
