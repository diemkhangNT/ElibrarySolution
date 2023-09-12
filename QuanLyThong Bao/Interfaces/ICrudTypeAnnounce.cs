using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Interfaces
{
    public interface ICrudTypeAnnounce
    {
        public Task<IEnumerable<LoaiThongBao>> Get_LoaiThongBaos();
        public Task<LoaiThongBao> Get_LoaiThongBao(string maLTB);
        public Task<LoaiThongBao> Put_LoaiThongBao(LoaiThongBao loaiThongBao);
        public Task<LoaiThongBao> Post_LoaiThongBao(LoaiThongBao loaiThongBao);
        public Task<bool> Delete_LoaiThongbao(string maLTB);
        
    }
}
