using QuanLyMonHoc.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyMonHoc.Dto
{
    public class LopHocDto
    {
        public string? MaLop { get; set; }
        [Required]
        public string TenLop { get; set; }
        public int? SiSo { get; set; }
        public string? GhiChu { get; set; }
        [Required]
        public string MaMH { get; set; }
        [Required]
        public string MaNK { get; set; }
    }
}
