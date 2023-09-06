using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonHoc.Model
{
    [Table("LopHoc")]
    public class LopHoc
    {
        [Key]
        public string MaLop { get; set; }
        [Required]
        [MaxLength(200)]
        public string TenLop { get; set; }

        public int SiSo { get; set; } = 0;
        [MaxLength(200)]
        public string? GhiChu { get; set; }
        public string MaMH { get; set; }
        [ForeignKey("MaMH")]
        public virtual MonHoc monHoc { get; set; }
        public string MaNK { get; set; }
        [ForeignKey("MaNK")]
        public virtual NienKhoa nienKhoa { get; set; }

    }
}
