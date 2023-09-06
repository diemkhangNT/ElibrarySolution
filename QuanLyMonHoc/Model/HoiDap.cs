using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonHoc.Model
{
    [Table("HoiDap")]
    public class HoiDap
    {
        [Key]
        public string MaCauHoi { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Không vượt quá 100 ký tự!")]
        public string TieuDe { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Không vượt quá 500 ký tự!")]
        public string NoiDung { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ThoiGian { get; set; }
        public string MaMH { get; set; }
        [ForeignKey("MaMH")]
        public virtual MonHoc monHoc { get; set; }
        public string MaHV { get; set; }

        public virtual IEnumerable<TraLoi> TraLois { get; set; }
    }
}
