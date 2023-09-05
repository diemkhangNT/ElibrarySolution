using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Data;
using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Services
{
    public class CrudService : ICrudService
    {
        private readonly VaiTroDbContext _dbContext;
        public CrudService(VaiTroDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<PhanQuyen> Delete_PhanQuyen(string maPQ)
        {
            throw new NotImplementedException();
        }

        public Task<VaiTro> Delete_VaiTro(string maVT)
        {
            throw new NotImplementedException();
        }

        public async Task<PhanQuyen> Get_PhanQuyen(string maPQ)
        {
            return await _dbContext.PhanQuyens.FindAsync(maPQ);
        }

        public async Task<IEnumerable<PhanQuyen>> Get_PhanQuyens()
        {
            return await _dbContext.PhanQuyens.ToListAsync();
        }

        public async Task<VaiTro> Get_VaiTro(string maVT)
        {
            return await _dbContext.VaiTros.FindAsync(maVT);
        }

        public async Task<IEnumerable<VaiTro>> Get_VaiTros()
        {
            return await _dbContext.VaiTros.ToListAsync();
        }

        public void Post_PhanQuyen(PhanQuyen phanQuyen)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            phanQuyen.MaPQ = "#QR" + num;
            _dbContext.PhanQuyens.Add(phanQuyen);
        }
        public bool PhanQuyenExists(string id)
        {
            return (_dbContext.PhanQuyens?.Any(e => e.MaPQ == id)).GetValueOrDefault();
        }

        public void Post_VaiTro(VaiTro vaiTro)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            vaiTro.MaVT = "#RO" + num;
            if (vaiTro.MoTa == "string")
                vaiTro.MoTa = null;
            DateTime today = DateTime.Now;
            vaiTro.NgayCapNhat = today;
            _dbContext.VaiTros.Add(vaiTro);
        }
        public bool VaiTroExists(string id)
        {
            return (_dbContext.VaiTros?.Any(e => e.MaVT == id)).GetValueOrDefault();
        }
        public Task<PhanQuyen> Put_PhanQuyen(PhanQuyen phanQuyen)
        {
            throw new NotImplementedException();
        }

        public void Put_VaiTro(VaiTro vaiTro)
        {
            DateTime today = DateTime.Now;
            vaiTro.NgayCapNhat = today;
        }
    }
}
