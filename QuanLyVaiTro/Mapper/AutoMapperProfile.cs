using AutoMapper;
using QuanLyVaiTro.Dto;
using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<VaiTro, VaiTroDto>();
            CreateMap<VaiTroDto, VaiTro>();
            CreateMap<PhanQuyen, PhanQuyenDto>();
            CreateMap<PhanQuyenDto, PhanQuyen>();
        }
    }
}
