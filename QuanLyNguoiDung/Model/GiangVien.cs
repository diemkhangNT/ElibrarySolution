using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNguoiDung.Model
{
    [Table("GiangVien")]
    public class GiangVien
    {
        [Key]
        public string? MaGV { get; set; }

        [StringLength(50)]
        public string TenGV { get; set; }

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