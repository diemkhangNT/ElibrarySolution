using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDeThi_KiemTra.Model
{
    [Table("TepCauHoi")]
    public class TepCauhoi
    {
        [Key]
        public int STT { get; set; }
        [MaxLength(200)]
        public string TenTep { get; set; }
    }
}
