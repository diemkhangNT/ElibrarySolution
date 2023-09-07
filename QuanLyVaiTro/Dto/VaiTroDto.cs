﻿using QuanLyVaiTro.Model;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVaiTro.Dto
{
    public class VaiTroDto
    {
        public string? MaVT { get; set; } = string.Empty;
        [Required]
        public string TenVT { get; set; } = string.Empty;
        public string? MoTa { get; set; } = string.Empty;
        public DateTime? NgayCapNhat { get; set; }

    }
}