using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Interface
{
    public interface ICrudHVService
    {
        public Task<IEnumerable<HocVien>> Get_HocViens();
        public Task<HocVien> Get_HovVien(string maHV);
        public Task<HocVien> Put_HocVien(HocVien hocVien);
        public Task<HocVien> Post_HocVien(HocVien hocVien);
        public Task<bool> Delete_HocVien(string maHV);
        public string GenarateJwtToken(HocVien user);
    }
}
