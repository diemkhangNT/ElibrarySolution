using AutoMapper;
using QuanLyBaiGiang_TaiNguyen.Dto;
using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ChuDeDto, ChuDe>();
            CreateMap<BaiGiangDto, BaiGiang>();
            CreateMap<TaiNguyenDto, TaiNguyen>();
        }
    }
}
