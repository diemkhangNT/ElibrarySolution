using System.ComponentModel.DataAnnotations;

namespace QuanLyNguoiDung.Dto
{
    public class LoginModelDto
    {
        [MaxLength(50)]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
