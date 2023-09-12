using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Interface
{
    public interface ICrudPhanQuyenService
    {
        //PhanQuyen
        public Task<IEnumerable<PhanQuyen>> Get_PhanQuyens();
        public Task<PhanQuyen> Get_PhanQuyen(string maPQ);
        public Task<PhanQuyen> Put_PhanQuyen(PhanQuyen phanQuyen);
        public Task<PhanQuyen> Post_PhanQuyen(PhanQuyen phanQuyen);
        public Task<bool> Delete_PhanQuyen(string maPQ);
        public bool PhanQuyenExists(string maPQ);
    }
}
