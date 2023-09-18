using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Interface
{
    public interface ICrudTepCauHoi
    {
        public Task<TepCauhoi> Get_TepCauhoi(int id);
        public Task<TepCauhoi> Post_TepCauhoi(TepCauhoi tepCauhoi);
        public Task<bool> Delete_TepCauhoi(int id);
    }
}
