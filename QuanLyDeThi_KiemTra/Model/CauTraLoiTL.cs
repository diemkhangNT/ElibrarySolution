using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDeThi_KiemTra.Model
{
    [Table("CauTraLoi")]
    public class CauTraLoiTL
    {
        [Key]
        public string MaCH { get; set; }
        [ForeignKey("MaCH")]
        public virtual CauHoiTL Hoi { get; set; }
        [Key]
        public string? MaHV { get; set; }
        public string? CauTL { get; set; }
        public string? FileCauTL { get; set; }

    }
}
