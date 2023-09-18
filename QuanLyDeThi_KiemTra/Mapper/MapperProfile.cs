using AutoMapper;
using QuanLyDeThi_KiemTra.Dto;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DeThiDto, DeThi>();
            CreateMap<CauHoiTuLuanDto, CauHoiTL>();
            CreateMap<TraLoiTracNghiemDto, TLTracNghiem>();
            CreateMap<CauHoiTracNghiemDto, CHTracNghiem>();
            CreateMap<TraLoiTuLuanDto, TraLoiTL>();
        }
    }
}
