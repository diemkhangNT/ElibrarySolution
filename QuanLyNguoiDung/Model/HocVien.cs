using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNguoiDung.Model
{
    [Table("HocVien")]
    public class HocVien
    {
        [Key]
        public string MaHV { get; set; }

        [StringLength(50)]
        public string TenHV { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }
        
        public string Password { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string SDTLienLac { get; set; }

        public string? AnhDaiDien { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayTao { get; set; }
        public string? MaLop { get ; set; }
    }
}
