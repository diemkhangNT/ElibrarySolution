using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Interface
{
    public interface IRefreshToken
    {
        public bool CheckRefreshToken(TokenModel model);
    }
}
