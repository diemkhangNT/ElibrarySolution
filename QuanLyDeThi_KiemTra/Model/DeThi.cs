using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDeThi_KiemTra.Model
{
    [Table("DeThi")]
    public class DeThi
    {
        [Key]
        public string MaDT { get; set; }
        [MaxLength(225)]
        public string TenDeThi { get; set; }
        public int? SLCauHoi { get; set; }
        public string? FileDeThi { get; set; }
        public int ThoiLuong { get; set; }
        public bool HinhThuc { get; set; } //Trắc nghiệm (true) hoặc tự luận (false)
        public string NguoiTao { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayTao { get; set; }
        public bool TinhTrangPD { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayPD { get; set; }
        public string MaMH { get; set; }
        public string MaBM { get; set; }

    }
}
