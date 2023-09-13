using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Interface
{
    public interface ICrudTraLoi
    {
        public Task<IEnumerable<TraLoi>> Get_TraLois();
        public Task<TraLoi> Get_TraLoi(string maTL);
        public Task<TraLoi> Put_TraLoi(TraLoi traLoi);
        public Task<TraLoi> Post_TraLoi(TraLoi traLoi);
        public Task<bool> Delete_TraLoi(string maTL);
    }
}
