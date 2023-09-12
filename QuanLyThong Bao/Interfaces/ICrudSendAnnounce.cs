using QuanLyThong_Bao.Model;

namespace QuanLyThong_Bao.Interfaces
{
    public interface ICrudSendAnnounce
    {
        public Task<IEnumerable<GuiThongBao>> Get_GuiTBs();
        public Task<GuiThongBao> Get_GuiTB(int STT);
        public Task<GuiThongBao> Put_GuiTB(GuiThongBao guiThongBao);
        public Task<GuiThongBao> Post_GuiTB(GuiThongBao guiThongBao);
        public Task<bool> Delete_GuiTB(int STT);
    }
}
