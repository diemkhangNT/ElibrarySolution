﻿using QuanLyMonHoc.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyMonHoc.Dto
{
    public class LopHocDto
    {
        public string TenLop { get; set; }
        public int? SiSo { get; set; }
        public string? GhiChu { get; set; }
        public string MaMH { get; set; }
        public string MaNK { get; set; }
    }
}
