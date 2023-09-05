using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVaiTro.Model
{
    [Table("PhanQuyen")]
    public class PhanQuyen
    {
        [Key]
        public string MaPQ { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenChucNang { get; set; }

        public bool Xem { get; set; } = false;
        public bool Xoa { get; set; } = false;
        public bool Sua { get; set; } = false;
        public bool ThemMoi { get; set; } = false;
        public bool PheDuyet { get; set; } = false;

        public string MaVT { get; set; }
        [ForeignKey("MaVT")]
        public virtual VaiTro VaiTro { get; set; }
    }
}
