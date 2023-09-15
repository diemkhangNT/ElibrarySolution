namespace QuanLyDeThi_KiemTra.Interface
{
    public interface IFileExtention
    {
        public Task<string> WriteFile(IFormFile file, string theLoai);
        public double SizeFile(IFormFile file);
        public void DeleteFile(string file, string theLoai);
    }
}
