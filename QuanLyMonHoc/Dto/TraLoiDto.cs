using QuanLyMonHoc.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyMonHoc.Dto
{
    public class TraLoiDto
    {
        public string? MaCauTL { get; set; }
        public string NoiDung { get; set; }
        public string MaCauHoi { get; set; }
        public string MaTacGia { get; set; }
    }
}
