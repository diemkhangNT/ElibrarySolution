using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Interface
{
    public interface ICrudLopHoc
    {
        public Task<IEnumerable<LopHoc>> Get_LopHocs();
        public Task<LopHoc> Get_LopHoc(string maLH);
        public Task<LopHoc> Put_LopHoc(LopHoc lopHoc);
        public Task<LopHoc> Post_LopHoc(LopHoc lopHoc);
        public Task<bool> Delete_LopHoc(string maLH);
    }
}
