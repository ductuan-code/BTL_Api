using BLL.DonUngTuyen;
using DTO.DonUngTuyen;
using Microsoft.AspNetCore.Mvc;

namespace NhaTuyenDungAPI.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class DonUngTuyenController : ControllerBase
    {
        private readonly IDonUngTuyenService _service;

        public DonUngTuyenController(IDonUngTuyenService service)
        {
            _service = service;
        }

        // Ứng viên nộp đơn
        [HttpPost("{maUngVien}")]
        public IActionResult TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto)
        {
            return _service.TaoDon(maUngVien, dto)
                ? Ok(true)
                : BadRequest(false);
        }

        // Nhà tuyển dụng xem danh sách đơn theo việc làm
        [HttpGet("job/{maViecLam}")]
        public IActionResult LayTheoViecLam(Guid maViecLam)
        {
            return Ok(_service.LayTheoViecLam(maViecLam));
        }

        // Xem chi tiết đơn
        [HttpGet("{maDon}")]
        public IActionResult LayChiTiet(Guid maDon)
        {
            var data = _service.LayChiTiet(maDon);
            return data == null ? NotFound() : Ok(data);
        }

        // Duyệt / từ chối
        [HttpPut("{maDon}/status")]
        public IActionResult CapNhatTrangThai(Guid maDon, [FromQuery] string trangThai)
        {
            return _service.CapNhatTrangThai(maDon, trangThai)
                ? Ok(true)
                : BadRequest(false);
        }
    }
}
