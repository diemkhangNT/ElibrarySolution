using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace QuanLyBaiGiang_TaiNguyen.Model
{
    public class ChuDe
    {
        [Key]
        public string MaCD { get; set; }
        public string TenCD { get; set; }
        public string MaMH { get; set; }
    }
}
