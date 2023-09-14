using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Interface
{
    public interface ICrudLDService
    {
        public Task<IEnumerable<Leadership>> Get_Leaderships();
        public Task<Leadership> Get_Leadership(string maLD);
        public Task<Leadership> Put_Leadership(Leadership leadership);
        public Task<Leadership> Post_Leadership(Leadership leadership);
        public Task<bool> Delete_Leadership(string maLD);
    }
}
