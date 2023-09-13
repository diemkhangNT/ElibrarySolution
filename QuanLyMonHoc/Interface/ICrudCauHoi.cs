using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Interface
{
    public interface ICrudCauHoi
    {
        public Task<IEnumerable<HoiDap>> Get_HoiDaps();
        public Task<HoiDap> Get_HoiDap(string maCH);
        public Task<HoiDap> Put_HoiDap(HoiDap hoiDap);
        public Task<HoiDap> Post_HoiDap(HoiDap hoiDap);
        public Task<bool> Delete_HoiDap(string maCH);
    }
}
