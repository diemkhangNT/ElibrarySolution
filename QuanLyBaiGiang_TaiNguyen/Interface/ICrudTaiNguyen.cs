using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Interface
{
    public interface ICrudTaiNguyen
    {
        public Task<IEnumerable<TaiNguyen>> Get_TaiNguyens();
        public Task<TaiNguyen> Get_TaiNguyen(string id);
        public Task<TaiNguyen> Put_TaiNguyen(TaiNguyen taiNguyen);
        public Task<TaiNguyen> XetDuyet_TaiNguyen(string id);
        public Task<TaiNguyen> Post_TaiNguyen(TaiNguyen taiNguyen, IFormFile file);
        public Task<bool> Delete_TaiNguyen(string id);
        bool TaiNguyenExists(string id);
    }
}
