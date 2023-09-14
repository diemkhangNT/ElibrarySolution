using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTepRiengTu.Model
{
    [Table("TepRiengTu")]
    public class TepRiengTu
    {
        [Key]
        public int STT { get; set; }
        [MaxLength(200)]
        public string TenTep { get; set; }
        public string Url { get; set; }
        public double KichThuoc { get; set; }
        [MaxLength(200)]
        public string NguoiTao { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayCapNhat { get; set; }
        [MaxLength(50)]
        public string TheLoai { get; set; }

    }
}
