using QuanLyDeThi_KiemTra.Interface;

namespace QuanLyDeThi_KiemTra.Services
{
    public class FileExtention : IFileExtention
    {
        public void DeleteFile(string file, string theLoai)
        {
            var exactPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{theLoai}", file);
            System.IO.File.Delete(exactPath);
        }

        public double SizeFile(IFormFile file)
        {
            if (file != null && file.Length > 1000000)
            {
                double sizeMB = (double)file.Length / 1048576;
                return Math.Round(sizeMB, 1);
            }
            else
            {
                double sizeKB = (double)file.Length / 1024;
                return Math.Round(sizeKB, 1);
            }
        }

        public async Task<string> WriteFile(IFormFile file, string theLoai)
        {
            string fileName = "";
            try
            {
                var extention = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                if (theLoai.Contains("DeThi") && (extention.ToLower().Contains(".mp4") || extention.ToLower().Contains(".mov") || extention.ToLower().Contains(".wmv")))
                    return fileName;
                else if (theLoai.Contains("BaiGiang") && (extention.ToLower().Contains(".docx") || extention.ToLower().Contains(".pdf") || extention.ToLower().Contains(".xlsx") || extention.ToLower().Contains(".xls") || extention.ToLower().Contains(".doc")))
                    return fileName;
                fileName = DateTime.Now.Ticks.ToString() + extention;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{theLoai}");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var exactPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{theLoai}", fileName);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
