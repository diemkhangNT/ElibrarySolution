using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNguoiDung.Model
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserID { get; set; }
        public string Token { get; set; }
        public string JwtID { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set;}
    }
}
