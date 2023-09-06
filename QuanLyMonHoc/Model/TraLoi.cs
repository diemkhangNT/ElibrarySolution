using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonHoc.Model
{
    [Table("HoiDap_TL")]
    public class TraLoi
    {
        [Key]
        public string MaCauTL { get; set; }
        [Required]
        [StringLength(200, ErrorMessage ="Không vượt quá 200 ký tự!")]
        public string NoiDung { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ThoiGian { get; set; }
        public string MaCauHoi { get; set; }
        [ForeignKey("MaCauHoi")]
        public virtual HoiDap hoiDap { get; set; }
        public string MaTacGia { get; set; }
    }
}
