using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDeThi_KiemTra.Model
{
    [Table("CHTracNghiem")]
    public class CHTracNghiem
    {
        [Key]
        public string MaCHTrN { get; set; }
        public string NoiDung { get; set; }
        public bool HinhThucTL { get; set; } //True: TChỉ một đáp án, false: Đđược nhiều đáp án
        public string? MaDeThi { get; set; }
        [ForeignKey("MaDeThi")]
        public virtual DeThi DeThi { get; set; }
    }
}
