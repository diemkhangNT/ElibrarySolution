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
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            hoiDap.MaCauHoi = "#CH" + num;
            hoiDap.ThoiGian = DateTime.Now;
        }

        public void AutoPK_LopHoc(LopHoc lopHoc)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            lopHoc.MaLop = "#LP" + num;
            if(lopHoc.SiSo == null)
                lopHoc.SiSo = 0;
        }

        public void AutoPK_MonHoc(MonHoc monHoc)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            monHoc.MaMH = "#DLK" + num;
            monHoc.NgayGuiPheDuyet = DateTime.Now;
        }

        public void AutoPK_NienKhoa(NienKhoa nienKhoa)
        {
            List<NienKhoa> nk = _dbContext.NienKhoa.ToList();
            if (nk.Count != 0)
            {
                string mank = nk[nk.Count - 1].MaNK;
                string a = mank.Substring(4);
                int b = Int32.Parse(a);
                nienKhoa.MaNK = "#NK0" + (b + 1);
            }
            else nienKhoa.MaNK = "#NK01";
        }

        public void AutoPK_TraLoi(TraLoi traLoi)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            traLoi.MaCauTL = "#TL" + num;
            traLoi.ThoiGian = DateTime.Now;
        }

        public bool IsCheckTime(int tgBD, int tgKT)
        {
            bool flag = _dbContext.NienKhoa.Any(s => s.TGBatDau == tgBD || s.TGKetThuc == tgKT);
            if (!flag)
            {
                if (tgBD < 0 || tgKT < 0 || tgBD >= tgKT)
                    return false;
                else
                {
                    if (tgBD / 1000 != 2 || tgKT / 1000 != 2)
                        return false;
                    else return true;
                }
            }
            else return false;
        }

        public bool IsCheckTime_put(int tgBD, int tgKT, string maNK)
        {
            bool flag = _dbContext.NienKhoa.Any(s => (s.TGBatDau == tgBD && s.MaNK !=  maNK ) || (s.TGKetThuc == tgKT && s.MaNK != maNK));
            if (!flag)
            {
                if (tgBD < 0 || tgKT < 0 || tgBD >= tgKT)
                    return false;
                else
                {
                    if (tgBD / 1000 != 2 || tgKT / 1000 != 2)
                        return false;
                    else return true;
                }
            }
            else return false;
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
                return true;
            }

            if (existing.MaMH != monHoc.MaMH && _dbContext.MonHocs.Any(x => x.TenMH == monHoc.TenMH))
            {
                return false;
            }
            _dbContext.MonHocs.Remove(existing);
            return true;
        }
    }
}
