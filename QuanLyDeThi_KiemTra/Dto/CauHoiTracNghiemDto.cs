using System.ComponentModel.DataAnnotations;

namespace QuanLyDeThi_KiemTra.Dto
{
    public class CauHoiTracNghiemDto
    {
        public string NoiDung { get; set; }
        public bool HinhThucTL { get; set; } //True: TChỉ một đáp án, false: Đđược nhiều đáp án
        public string DoKho { get; set; }
        public string? MaDeThi { get; set; }
    }
}
