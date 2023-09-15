using QuanLyBaiGiang_TaiNguyen.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBaiGiang_TaiNguyen.Dto
{
    public class TaiNguyenDto
    {
        public string TieuDe { get; set; }
        public string? MaBG { get; set; }
        public string? MaCD { get; set; }
        public string? MaMH { get; set; }
    }
}
