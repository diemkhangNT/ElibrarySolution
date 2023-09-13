using System.ComponentModel.DataAnnotations;

namespace QuanLyNguoiDung.Dto
{
    public class HocVienDto
    {
        public string TenHV { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string SDTLienLac { get; set; }

        public string? MaLop { get; set; }
    }
}
