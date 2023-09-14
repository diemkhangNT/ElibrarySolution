using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Dto;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadershipsController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IExtensionServices _extensionServices;
        private readonly ICrudLDService _crudService;
        private readonly IMapper _mapper;

        public LeadershipsController(UserDBContext context, IExtensionServices extensionServices, IMapper mapper, ICrudLDService crudService)
        {
            _context = context;
            _extensionServices = extensionServices;
            _mapper = mapper;
            _crudService = crudService;
        }

        // GET: api/Leaderships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leadership>>> Getleaderships()
        {
            if (_context.leaderships == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_Leaderships();
            return listLTB.ToList();
        }

        // GET: api/Leaderships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Leadership>> GetLeadership(string id)
        {
            Leadership leadership = await _crudService.Get_Leadership(id);
            if (leadership == null)
            {
                return NotFound("Không tìm thấy leadership có id = " + id + "!");
            }
            else return leadership;
        }

        // PUT: api/Leaderships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeadership(string id,[FromForm] LeadershipDto leadershipDto, IFormFile? HinhDaiDien)
        {
            Leadership leadership = _mapper.Map<Leadership>(leadershipDto);
            if (!_extensionServices.LeadershipExists(id))
            {
                return BadRequest("Không tồn tại mã giảng viên này!");
            }
            if (_extensionServices.IsEmailLDUnique(leadership.Email))
            {
                return BadRequest("Email này đã được sử dụng! Vui lòng nhập email khác!");
            }
            else if (_extensionServices.IsEmailLDUnique(leadership.Username))
            {
                return BadRequest("Tên đăng nhập này đã được sử dụng! Vui lòng nhập tên khác!");
            }
            else if (!_extensionServices.IsNumberPhone(leadership.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            else if (!_extensionServices.ValidatePassword(leadership.Password))
            {
                return BadRequest("Password phải ít nhất 8 ký tự, ít nhất một ký tự in hoa, chữ thường, số và ký tự đặt biệt!!");
            }
            if (HinhDaiDien != null)
            {
                _extensionServices.UploadImageLD(leadership, HinhDaiDien);
            }
            try
            {
                leadership.MaLD = id;
                await _crudService.Put_Leadership(leadership);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_extensionServices.GiangVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Thông tin đã được cập nhật!");
        }

        // POST: api/Leaderships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Leadership>> PostLeadership([FromForm] LeadershipDto leadershipDto, IFormFile? HinhDaiDien)
        {
            Leadership leadership = _mapper.Map<Leadership>(leadershipDto);
            if (_context.GiangViens == null)
            {
                return Problem("Entity set 'UserDBContext.GiangViens'  is null.");
            }
            if (_extensionServices.IsEmailGVUnique(leadership.Email))
            {
                return BadRequest("Email này đã được sử dụng! Vui lòng nhập email khác!");
            }
            else if (_extensionServices.IsUserNameGVUnique(leadership.Username))
            {
                return BadRequest("Tên đăng nhập này đã được sử dụng! Vui lòng nhập tên khác!");
            }
            else if (!_extensionServices.IsNumberPhone(leadership.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            else if (!_extensionServices.ValidatePassword(leadership.Password))
            {
                return BadRequest("Password phải ít nhất 8 ký tự, ít nhất một ký tự in hoa, chữ thường, số và ký tự đặt biệt!!");
            }
            if (HinhDaiDien != null)
            {
                _extensionServices.UploadImageLD(leadership, HinhDaiDien);
            }
            try
            {
                _extensionServices.AutoPK_Leader(leadership);
                await _crudService.Post_Leadership(leadership);
            }
            catch (DbUpdateException)
            {
                if (_extensionServices.GiangVienExists(leadership.MaLD))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetLeadership", new { id = leadership.MaLD }, leadership);
        }

        // DELETE: api/Leaderships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeadership(string id)
        {
            bool flag = await _crudService.Delete_Leadership(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }

    }
}
