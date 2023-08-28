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
    }
}
