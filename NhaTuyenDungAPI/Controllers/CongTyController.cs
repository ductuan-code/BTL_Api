using BLL.CongTy;
using DTO.CongTy;
using Microsoft.AspNetCore.Mvc;

namespace NhaTuyenDungAPI.Controllers
{
    [ApiController]
    [Route("api/employer/companies")]
    public class CongTyController : ControllerBase
    {
        private readonly ICongTyService _service;

        public CongTyController(ICongTyService service)
        {
            _service = service;
        }

        [HttpGet("{maNguoiDung}")]
        public IActionResult GetMyCompanies(Guid maNguoiDung)
        {
            return Ok(_service.GetMyCompanies(maNguoiDung));
        }

        [HttpGet("{maNguoiDung}/{maCongTy}")]
        public IActionResult Detail(Guid maNguoiDung, Guid maCongTy)
        {
            var data = _service.GetDetail(maCongTy, maNguoiDung);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost("{maNguoiDung}")]
        public IActionResult Create(Guid maNguoiDung, [FromBody] TaoCongTyDto dto)
        {
            return _service.TaoCongTy(dto, maNguoiDung)
                ? Ok("Tạo công ty thành công")
                : BadRequest();
        }

        [HttpPut("{maNguoiDung}/{maCongTy}")]
        public IActionResult Update(Guid maNguoiDung, Guid maCongTy, [FromBody] TaoCongTyDto dto)
        {
            return _service.CapNhatCongTy(maCongTy, dto, maNguoiDung)
                ? Ok("Cập nhật thành công")
                : BadRequest("Không có quyền hoặc không tồn tại");
        }
    }
}
