using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Interface
{
    public interface ICrudChuDe
    {
        public Task<IEnumerable<ChuDe>> Get_ChuDes();
        public Task<ChuDe> Get_ChuDe(string maCD);
        public Task<ChuDe> Put_ChuDe(ChuDe chuDe);
        public Task<ChuDe> Post_ChuDe(ChuDe chuDe);
        public Task<bool> Delete_ChuDe(string maCD);
        bool ChuDeExists(string id);
    }
}
