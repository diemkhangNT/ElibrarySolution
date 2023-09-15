namespace QuanLyDeThi_KiemTra.Dto
{
    public class CauHoiTuLuanDto
    {
        public string NoiDung { get; set; }
        public bool HinhThucTL { get; set; } //True: Tải tệp lên, false: Điền đáp án trự tiếp
        public string? GioiHanSoTu { get; set; } //Mở ra khi HinhThucTL == false
        public string MaDeThi { get; set; }
    }
}
