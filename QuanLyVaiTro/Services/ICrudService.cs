using Microsoft.AspNetCore.Mvc;
using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Services
{
    public interface ICrudService
    {
        //Vai tro
        public Task<IEnumerable<VaiTro>> Get_VaiTros();
        public Task<VaiTro> Get_VaiTro(string maVT);
        public void Put_VaiTro(VaiTro vaiTro);
        public void Post_VaiTro(VaiTro vaiTro);
        public Task<VaiTro> Delete_VaiTro(string maVT);
        public bool VaiTroExists(string maVT);
        //PhanQuyen
        public Task<IEnumerable<PhanQuyen>> Get_PhanQuyens();
        public Task<PhanQuyen> Get_PhanQuyen(string maPQ);
        public Task<PhanQuyen> Put_PhanQuyen(PhanQuyen phanQuyen);
        public void Post_PhanQuyen(PhanQuyen phanQuyen);
        public Task<PhanQuyen> Delete_PhanQuyen(string maPQ);
        public bool PhanQuyenExists(string maPQ);
    }
}
