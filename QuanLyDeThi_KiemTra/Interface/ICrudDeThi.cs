using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Interface
{
    public interface ICrudDeThi
    {
        public Task<IEnumerable<DeThi>> Get_DeThis();
        public Task<DeThi> Get_DeThi(string id);
        public Task<DeThi> Put_DeThi(DeThi deThi);
        public Task<DeThi> Post_DeThi(DeThi deThi);
        public Task<bool> Delete_DeThi(string id);
        public Task<DeThi> XetDuyet_DeThi(string id);
        bool DeThiExists(string id);
    }
}
