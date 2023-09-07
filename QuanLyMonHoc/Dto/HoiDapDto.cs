using QuanLyMonHoc.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyMonHoc.Dto
{
    public class HoiDapDto
    {
        public string? MaCauHoi { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string MaMH { get; set; }
        public string MaHV { get; set; }
    }
}
