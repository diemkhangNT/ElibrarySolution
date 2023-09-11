using System.ComponentModel.DataAnnotations;

namespace QuanLyThong_Bao.Dto
{
    public class ThongBaoDto
    {
        public string? MaTB { get; set; }
        [MinLength(5), MaxLength(200)]
        public string TieuDe { get; set; }
        [MinLength(20)]
        public string NoiDung { get; set; }
        public string? TrangThai { get; set; } //Đọc, chưa đọc, đã đọc, đã gửi, đã hủy
        public string MaLTB { get; set; }
    }
}
