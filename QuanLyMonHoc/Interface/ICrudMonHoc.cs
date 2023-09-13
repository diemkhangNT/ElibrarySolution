using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Interface
{
    public interface ICrudMonHoc
    {
        public Task<IEnumerable<MonHoc>> Get_MonHocs();
        public Task<MonHoc> Get_MonHoc(string maMH);
        public Task<MonHoc> Put_MonHoc(MonHoc monHoc);
        public Task<MonHoc> Post_MonHoc(MonHoc monHoc);
        public Task<bool> Delete_MonHoc(string maMH);
    }
}
