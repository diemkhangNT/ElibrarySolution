using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Interface
{
    public interface ICrudTraLoiTracNghiem 
    {
        public Task<IEnumerable<TLTracNghiem>> Get_TLTracNghiems();
        public Task<TLTracNghiem> Get_TLTracNghiem(string id);
        public Task<TLTracNghiem> Put_TLTracNghiem(TLTracNghiem tLTracNghiem);
        public Task<TLTracNghiem> Post_TLTracNghiem(TLTracNghiem tLTracNghiem);
        public Task<bool> Delete_TLTracNghiem(string id);
        bool TLTracNghiemExists(string id);
    }
}
