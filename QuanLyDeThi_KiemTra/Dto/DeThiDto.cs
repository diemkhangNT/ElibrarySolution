using System.ComponentModel.DataAnnotations;

namespace QuanLyDeThi_KiemTra.Dto
{
    public class DeThiDto
    {
        public string TenDeThi { get; set; }
        public int? SLCauHoi { get; set; }
        public bool HinhThuc { get; set; } //Trắc nghiệm (true) hoặc tự luận (false)
        public int ThangDiem { get; set; }
        public int ThoiLuong { get; set; }
        public string MaMH { get; set; }
        public string MaBM { get; set; }
    }
}
