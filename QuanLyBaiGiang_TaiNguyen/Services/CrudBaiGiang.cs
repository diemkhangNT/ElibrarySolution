using Microsoft.EntityFrameworkCore;
using QuanLyBaiGiang_TaiNguyen.Data;
using QuanLyBaiGiang_TaiNguyen.Interface;
using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Services
{
    public class CrudBaiGiang : ICrudBaiGiang
    {
        private readonly TaiNguyenDbContext _context;
        private readonly IFileExtension _fileExtension;

        public CrudBaiGiang(TaiNguyenDbContext context, IFileExtension fileExtension)
        {
            _context = context;
            _fileExtension = fileExtension;
        }
        public bool BaiGiangExists(string id)
        {
            return (_context.BaiGiangs?.Any(e => e.MaBG == id)).GetValueOrDefault();
        }

        public async Task<bool> Delete_BaiGiang(string id)
        {
            if (_context.BaiGiangs == null)
            {
                return false;
            }
            var baiGiang = await _context.BaiGiangs.FindAsync(id);
            if (baiGiang == null)
            {
                return false;
            }
            _fileExtension.DeleteFile(baiGiang.TenFile, "BaiGiang");
            _context.BaiGiangs.Remove(baiGiang);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BaiGiang> Get_BaiGiang(string id)
        {
            var baiGiang = await _context.BaiGiangs.FindAsync(id);
            return baiGiang;
        }

        public async Task<IEnumerable<BaiGiang>> Get_BaiGiangs()
        {
            return await _context.BaiGiangs.ToListAsync();
        }

        public async Task<BaiGiang> Post_BaiGiang(BaiGiang baiGiang, IFormFile file)
        {
            Random rnd = new Random();
            string maBG = "#BG" + rnd.Next(10, 1000000000);
            baiGiang.MaBG = maBG;
            baiGiang.NgayTao = DateTime.Now;
            baiGiang.NgayGuiPD = null;
            baiGiang.TinhTrangPD = false;
            string size = _fileExtension.SizeFile(file, "");
            string kt = size.Split('_')[0];
            string dvt = size.Split("_")[1];
            baiGiang.KichThuoc = double.Parse(kt.ToString());
            baiGiang.DonViTinh = dvt;
            var extention = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            baiGiang.TheLoai = extention;
            _context.BaiGiangs.Add(baiGiang);
            _context.SaveChanges();
            return baiGiang;
        }

        public async Task<BaiGiang> Put_BaiGiang(BaiGiang baiGiang)
        {
            var existBG = _context.BaiGiangs.FirstOrDefault(x => x.MaBG == baiGiang.MaBG);
            baiGiang.KichThuoc = existBG.KichThuoc;
            baiGiang.NgayGuiPD = null;
            baiGiang.TinhTrangPD = false;
            baiGiang.NgayTao = DateTime.Now;
            baiGiang.TheLoai = existBG.TheLoai;
            baiGiang.TenFile = existBG.TenFile;
            baiGiang.DonViTinh = existBG.DonViTinh;
            _context.BaiGiangs.Remove(existBG);
            //_context.SaveChanges();
            _context.Entry(baiGiang).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return baiGiang;
        }

        public async Task<BaiGiang> XetDuyet_BaiGiang(string id)
        {
            BaiGiang baiGiang = await _context.BaiGiangs.Where(s => s.MaBG == id).FirstOrDefaultAsync();
            baiGiang.NgayGuiPD = DateTime.Now;
            baiGiang.TinhTrangPD = true;
            _context.Entry(baiGiang).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return baiGiang;
        }
    }
}
