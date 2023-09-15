using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Interface
{
    public interface ICrudBaiGiang
    {
        public Task<IEnumerable<BaiGiang>> Get_BaiGiangs();
        public Task<BaiGiang> Get_BaiGiang(string id);
        public Task<BaiGiang> Put_BaiGiang(BaiGiang baiGiang);
        public Task<BaiGiang> XetDuyet_BaiGiang(string id);
        public Task<BaiGiang> Post_BaiGiang(BaiGiang baiGiang, IFormFile file);
        public Task<bool> Delete_BaiGiang(string id);
        bool BaiGiangExists(string id);
    }
}
