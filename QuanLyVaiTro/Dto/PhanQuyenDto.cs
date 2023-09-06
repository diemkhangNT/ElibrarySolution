using System.ComponentModel.DataAnnotations;

namespace QuanLyVaiTro.Dto
{
    public class PhanQuyenDto
    {
        public string? MaPQ { get; set; }
        [Required]
        public string TenChucNang { get; set; }
        public bool Xem { get; set; }
        public bool Xoa { get; set; } 
        public bool Sua { get; set; } 
        public bool ThemMoi { get; set; } 
        public bool PheDuyet { get; set; }
        public string MaVT { get; set; }
    }
}
