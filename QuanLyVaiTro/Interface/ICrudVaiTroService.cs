using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Interface
{
    public interface ICrudVaiTroService
    {
        //Vai tro
        public Task<IEnumerable<VaiTro>> Get_VaiTros();
        public Task<VaiTro> Get_VaiTro(string maVT);
        public Task<VaiTro> Put_VaiTro(VaiTro vaiTro);
        public Task<VaiTro> Post_VaiTro(VaiTro vaiTro);
        public Task<bool> Delete_VaiTro(string maVT);
        public bool VaiTroExists(string maVT);
    }
}
