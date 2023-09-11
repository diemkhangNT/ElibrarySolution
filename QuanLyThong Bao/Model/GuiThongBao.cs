using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThong_Bao.Model
{
    [Table("GuiThongBao")]
    public class GuiThongBao
    {
        [Key]
        public int STT { get; set; }
        public string MaNguoiGui { get; set; }
        public string MaNguoiNhan { get; set; }
        public string MaTB { get; set; }
        [ForeignKey("MaTB")]
        public virtual ThongBao ThongBao { get; set; }
    }
}
