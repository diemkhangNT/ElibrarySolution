using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Interface
{
    public interface ICrudTraLoiTuLuan
    {
        public Task<IEnumerable<TraLoiTL>> Get_TraLoiTLs();
        public Task<TraLoiTL> Get_TraLoiTL(string id);
        public Task<TraLoiTL> Put_TraLoiTL(TraLoiTL traLoiTL);
        public Task<TraLoiTL> Post_TraLoiTL(TraLoiTL traLoiTL);
        public Task<bool> Delete_TraLoiTL(string id);
        bool TraLoiTLExists(string id);
        bool Check_HinhThucTL(string id);
    }
}
