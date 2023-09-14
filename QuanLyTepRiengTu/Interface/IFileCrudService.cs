using QuanLyTepRiengTu.Model;

namespace QuanLyTepRiengTu.Interface
{
    public interface IFileCrudService
    {
        public void Post_Data(TepRiengTu tepRiengTu, IFormFile file);
        public Task<string> WriteFile(IFormFile file);
        public double SizeFile(IFormFile file);
        public Task<IEnumerable<TepRiengTu>> Get_TepRiengTus();
        public Task<TepRiengTu> Get_TepRiengTu(int stt);
        public Task<TepRiengTu> Put_TepRiengTu(TepRiengTu tepRiengTu);
        public Task<bool> Delete_TepRiengTu(int stt);
        bool TepRTExists(int stt);
    }
}
