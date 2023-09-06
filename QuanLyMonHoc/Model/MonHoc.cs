using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonHoc.Model
{
    [Table("MonHoc")]
    public class MonHoc
    {
        [Key]
        public string MaMH { get; set; }

        [Required]
        public string TenMH { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayGuiPheDuyet { get; set; }

        public bool TinhTrang { get; set; } = false;

        public string? MoTa { get; set; }
        public string? MaGV { get; set; }

        public virtual IEnumerable<LopHoc> LopHocs { get; set;}
        public virtual IEnumerable<HoiDap> HoiDaps { get; set;}

    }
}
