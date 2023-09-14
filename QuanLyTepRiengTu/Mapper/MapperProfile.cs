using AutoMapper;
using QuanLyTepRiengTu.Dto;
using QuanLyTepRiengTu.Model;

namespace QuanLyTepRiengTu.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() 
        { 
            CreateMap<TepRiengTuDto, TepRiengTu>();
        }
    }
}
