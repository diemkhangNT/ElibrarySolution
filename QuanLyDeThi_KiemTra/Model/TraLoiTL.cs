using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDeThi_KiemTra.Model
{
    [Table("CauTraLoiTL")]
    public class TraLoiTL
    {
        [Key]
        public string MaCH { get; set; }
        [ForeignKey("MaCH")]
        public virtual CauHoiTL CauHoiTL { get; set; }
        public string? CauTL { get; set; }
        public string? FileCauTL { get; set; }

    }
}
