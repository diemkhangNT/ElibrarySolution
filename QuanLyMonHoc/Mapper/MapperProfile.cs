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

        }
    }
}
