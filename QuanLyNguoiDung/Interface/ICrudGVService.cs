using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Interface
{
    public interface ICrudGVService
    {
        public Task<IEnumerable<GiangVien>> Get_GiangViens();
        public Task<GiangVien> Get_GiangVien(string maGV);
        public Task<GiangVien> Put_GiangVien(GiangVien giangVien);
        public Task<GiangVien> Post_GiangVien(GiangVien giangVien);
        public Task<bool> Delete_GiangVien(string maGV);
        public Task<TokenModel> GenarateJwtToken(GiangVien user);
        public string GenerateRefreshToken();
    }
}
