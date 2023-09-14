using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNguoiDung.Model
{
    [Table("Leadership")]
    public class Leadership
    {
        [Key]
        public string MaLD { get; set; }
        [StringLength(50)]
        public string TenLD{ get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10)]
        public string SDTLienLac { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        public string Password { get; set; }

        public string? DiaChi { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgaySinh { get; set; }

        public bool? GioiTinh { get; set; } = true;

        public string? HinhDaiDien { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayHopTac { get; set; }

        public bool? TrangThaiHD { get; set; } = true;
    }
}
