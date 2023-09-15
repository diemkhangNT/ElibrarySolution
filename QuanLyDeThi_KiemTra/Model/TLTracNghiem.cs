using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDeThi_KiemTra.Model
{
    public class TLTracNghiem
    {

        [Key]
        public string MaCH { get; set; }
        [ForeignKey("MaCH")]
        public virtual CHTracNghiem Hoi { get; set; }
        public char TieuDeA { get; set; }
        public string NoiDungA { get; set; }
        public bool LaDapAnA { get; set; }

        public char TieuDeB { get; set; }
        public string NoiDungB { get; set; }
        public bool LaDapAnB { get; set; }

        public char TieuDeC { get; set; }
        public string NoiDungC { get; set; }
        public bool LaDapAnC { get; set; }

        public char TieuDeD { get; set; }
        public string NoiDungD { get; set; }
        public bool LaDapAnD { get; set; }
    }
}
