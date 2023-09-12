using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Interfaces
{
    public interface ICrudAnnounce
    {
        public Task<IEnumerable<ThongBao>> Get_ThongBaos();
        public Task<ThongBao> Get_ThongBao(string maTB);
        public Task<ThongBao> Put_ThongBao(ThongBao thongBao);
        public Task<ThongBao> Post_ThongBao(ThongBao thongBao);
        public Task<bool> Delete_ThongBao(string maTB);
    }
}
