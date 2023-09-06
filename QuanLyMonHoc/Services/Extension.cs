using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Services
{
    public class Extension : IExtension
    {
        private readonly MonHocDbContext _dbContext;
        public Extension(MonHocDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AutoPK_HoiDap(HoiDap hoiDap)
        {
            throw new NotImplementedException();
        }

        public void AutoPK_LopHoc(LopHoc lopHoc)
        {
            throw new NotImplementedException();
        }

        public void AutoPK_MonHoc(MonHoc monHoc)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            monHoc.MaMH = "#DLK" + num;
            monHoc.NgayGuiPheDuyet = DateTime.Now;
            if(monHoc.TinhTrang == null)
            {
                monHoc.TinhTrang = false;
            }
        }

        public void AutoPK_NienKhoa(NienKhoa nienKhoa)
        {
            throw new NotImplementedException();
        }

        public void AutoPK_TraLoi(TraLoi traLoi)
        {
            throw new NotImplementedException();
        }

        public bool IsCheckTime(int tgBD, int tgKT)
        {
            throw new NotImplementedException();
        }

        public bool IsExistNameMonHoc_Post(string tenMH)
        {
            return _dbContext.MonHocs.Any(u => u.TenMH == tenMH);
        }

        public bool IsExistNameMonHoc_Put(MonHoc monHoc)
        {
            var existing = _dbContext.MonHocs.FirstOrDefault(x => x.TenMH == monHoc.TenMH);

            if (existing == null)
            {
                return false;
            }

            if (existing.MaMH != monHoc.MaMH && _dbContext.MonHocs.Any(x => x.TenMH == monHoc.TenMH))
            {
                return false;
            }
            return true;
        }
    }
}
