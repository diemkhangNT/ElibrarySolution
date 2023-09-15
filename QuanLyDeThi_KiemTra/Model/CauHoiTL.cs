using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDeThi_KiemTra.Model
{
    [Table("CauHoi")]
    public class CauHoiTL
    {
        [Key]
        public string MaCH { get; set; }
        public string NoiDung { get; set; }
        public bool HinhThucTL { get; set; } //True: Tải tệp lên, false: Điền đáp án trự tiếp
        public string? GioiHanSoTu { get; set; } //Mở ra khi HinhThucTL == false
        public string MaDeThi { get; set; }
        [ForeignKey("MaDeThi")]
        public virtual DeThi DeThi { get; set; }
    }
}
