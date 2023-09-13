using AutoMapper;
using QuanLyMonHoc.Dto;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        {
            CreateMap<MonHoc, MonHocDto>();
            CreateMap<MonHocDto, MonHoc>();
            CreateMap<LopHoc, LopHocDto>();
            CreateMap<LopHocDto, LopHoc>();
            CreateMap<HoiDap, HoiDapDto>();
            CreateMap<HoiDapDto, HoiDap>();
            CreateMap<TraLoiDto, TraLoi>();
            CreateMap<TraLoi, TraLoiDto>();
            CreateMap<NienKhoaDto, NienKhoa>();
            CreateMap<NienKhoa, NienKhoaDto>();
        }
    }
}
