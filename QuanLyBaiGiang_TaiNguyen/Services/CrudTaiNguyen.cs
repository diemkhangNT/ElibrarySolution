using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyBaiGiang_TaiNguyen.Data;
using QuanLyBaiGiang_TaiNguyen.Interface;
using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Services
{
    public class CrudTaiNguyen : ICrudTaiNguyen
    {
        private readonly TaiNguyenDbContext _context;
        private readonly IFileExtension _fileExtension;

        public CrudTaiNguyen(TaiNguyenDbContext context, IFileExtension fileExtension)
        {
            _context = context;
            _fileExtension = fileExtension;
        }

        public async Task<bool> Delete_TaiNguyen(string id)
        {
            if (_context.TaiNguyens == null)
            {
                return false;
            }
            var taiNguyen = await _context.TaiNguyens.FindAsync(id);
            if (taiNguyen == null)
            {
                return false;
            }
            _fileExtension.DeleteFile(taiNguyen.TenFile, "TaiNguyen");
            _context.TaiNguyens.Remove(taiNguyen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaiNguyen> Get_TaiNguyen(string id)
        {
            var taiNguyen = await _context.TaiNguyens.FindAsync(id);
            return taiNguyen;
        }

        public async Task<IEnumerable<TaiNguyen>> Get_TaiNguyens()
        {
            return await _context.TaiNguyens.ToListAsync();
        }

        public async Task<TaiNguyen> Post_TaiNguyen(TaiNguyen taiNguyen, IFormFile file)
        {
            Random rnd = new Random();
            string maTN = "#TN" + rnd.Next(10, 1000000000);
            taiNguyen.MaTN = maTN;
            taiNguyen.NgayTao = DateTime.Now;
            taiNguyen.NgayGuiPD = null;
            taiNguyen.TinhTrangPD = false;
            string size = _fileExtension.SizeFile(file, "");
            string kt = size.Split('_')[0];
            string dvt = size.Split("_")[1];
            taiNguyen.KichThuoc = double.Parse(kt.ToString());
            taiNguyen.DonViTinh = dvt;
            var extention = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            taiNguyen.TheLoai = extention;
            _context.TaiNguyens.Add(taiNguyen);
            _context.SaveChanges();
            return taiNguyen;
        }

        public async Task<TaiNguyen> Put_TaiNguyen(TaiNguyen taiNguyen)
        {
            var existTN = _context.TaiNguyens.FirstOrDefault(x => x.MaTN == taiNguyen.MaTN);
            taiNguyen.KichThuoc = existTN.KichThuoc;
            taiNguyen.NgayGuiPD = null;
            taiNguyen.TinhTrangPD = false;
            taiNguyen.NgayTao = DateTime.Now;
            taiNguyen.TheLoai = existTN.TheLoai;
            taiNguyen.TenFile = existTN.TenFile;
            taiNguyen.DonViTinh = existTN.DonViTinh;
            _context.TaiNguyens.Remove(existTN);
            //_context.SaveChanges();
            _context.Entry(taiNguyen).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return taiNguyen;
        }

        public async Task<TaiNguyen> XetDuyet_TaiNguyen(string id)
        {
            TaiNguyen taiNguyen = await _context.TaiNguyens.Where(s => s.MaTN == id).FirstOrDefaultAsync();
            taiNguyen.NgayGuiPD = DateTime.Now;
            taiNguyen.TinhTrangPD = true;
            _context.Entry(taiNguyen).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return taiNguyen;
        }

            public bool TaiNguyenExists(string id)
        {
            return (_context.TaiNguyens?.Any(e => e.MaTN == id)).GetValueOrDefault();
        }
    }
}
