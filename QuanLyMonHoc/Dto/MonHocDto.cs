using System.ComponentModel.DataAnnotations;

namespace QuanLyMonHoc.Dto
{
    public class MonHocDto
    {
        public string? MaMH { get; set; }

        [Required]
        public string TenMH { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayGuiPheDuyet { get; set; }

        public bool? TinhTrang { get; set; }

        public string? MoTa { get; set; }
        public string? MaGV { get; set; }
    }
}
