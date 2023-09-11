using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThong_Bao.Model
{
    [Table("ThongBao")]
    public class ThongBao
    {
        [Key]
        public string MaTB { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayTao { get; set; }
        public string? TrangThai { get; set; } //Đọc, chưa đọc, đã đọc, đã gửi, đã hủy
        public string MaLTB { get; set; }
        [ForeignKey("MaLTB")]
        public virtual LoaiThongBao LoaiThongBao { get; set; }

        
    }
}
