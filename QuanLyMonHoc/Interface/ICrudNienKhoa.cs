using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Interface
{
    public interface ICrudNienKhoa
    {
        public Task<IEnumerable<NienKhoa>> Get_NienKhoas();
        public Task<NienKhoa> Get_NienKhoa(string maNK);
        public Task<NienKhoa> Put_NienKhoa(NienKhoa nienKhoa);
        public Task<NienKhoa> Post_NienKhoa(NienKhoa nienKhoa);
        public Task<bool> Delete_NienKhoa(string maNK);
    }
}
