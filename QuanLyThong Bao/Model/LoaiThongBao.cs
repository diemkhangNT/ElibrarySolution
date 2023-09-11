using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThong_Bao.Model
{
    [Table("LoaiThongbao")]
    public class LoaiThongBao
    {
        [Key]
        public string MaLTB { get; set; }
        [Required]
        public string TenLTB { get; set; }
        public virtual IEnumerable<ThongBao> ThongBaos { get; set; }
    }
}
