using AutoMapper;
using QuanLyNguoiDung.Dto;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<GiangVienDto, GiangVien>();
            CreateMap<HocVienDto, HocVien>();
        }
    }
}
