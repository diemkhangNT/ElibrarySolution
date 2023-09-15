﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBaiGiang_TaiNguyen.Model
{
    public class TaiNguyen
    {
        [Key]
        public string MaTN { get; set; }
        [MaxLength(255)]
        public string TieuDe { get; set; }
        public double KichThuoc { get; set; }
        [MaxLength(2)]
        public string DonViTinh { get; set; }
        [MaxLength(25)]
        public string TheLoai { get; set; }
        public string TenFile { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayTao { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgayGuiPD { get; set; }
        public bool TinhTrangPD { get; set; }
        public string? MaBG { get; set; }
        [ForeignKey("MaBG")]
        public virtual BaiGiang BaiGiang { get; set; }
        public string? MaCD { get; set; }
        [ForeignKey("MaCD")]
        public virtual ChuDe ChuDe { get; set; }
        public string? MaMH { get; set; }
    }
}
