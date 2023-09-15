namespace QuanLyBaiGiang_TaiNguyen.Interface
{
    public interface IFileExtension
    {
        public Task<string> WriteFile(IFormFile file, string theLoai);
        public string SizeFile(IFormFile file, string DVT);
        public void DeleteFile(string file, string theLoai);
    }
}
