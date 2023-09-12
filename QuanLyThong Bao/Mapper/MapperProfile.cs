using AutoMapper;
using QuanLyThong_Bao.Dto;
using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<ThongBaoDto, ThongBao>();
            CreateMap<LoaiTBDto, LoaiThongBao>();
            CreateMap<GuiThongBaoDto, GuiThongBao>();
        }
    }
}
