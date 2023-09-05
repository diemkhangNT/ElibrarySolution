using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVaiTro.Model
{
    [Table("VaiTro")]
    public class VaiTro
    {
        [Key]
        public string MaVT { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenVT { get; set; }

        public string? MoTa { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayCapNhat { get; set; }
          
        public virtual IEnumerable<PhanQuyen> PhanQuyens { get; set; }  

    }
}
