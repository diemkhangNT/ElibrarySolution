using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Interface
{
    public interface ICrudCauHoiTracNghiem
    {
        public Task<IEnumerable<CHTracNghiem>> Get_CHTracNghiems();
        public Task<CHTracNghiem> Get_CHTracNghiem(string id);
        public Task<CHTracNghiem> Put_CHTracNghiem(CHTracNghiem cHTracNghiem);
        public Task<CHTracNghiem> Post_CHTracNghiem(CHTracNghiem cHTracNghiem);
        public Task<bool> Delete_CHTracNghiem(string id);
        bool CHTracNghiemExists(string id);
    }
}
