using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonHoc.Model
{
    [Table("NienKhoa")]
    public class NienKhoa
    {
        [Key]
        public string? MaNK { get; set; }
        [Required]
        public int TGBatDau { get; set; }
        [Required]
        public int TGKetThuc { get; set; }

        public virtual IEnumerable<LopHoc>? LopHocs { get; set; }
    }
}
