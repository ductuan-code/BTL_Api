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
        public IActionResult Get(Guid maNguoiDung)
        {
            return Ok(_service.GetMyCompanies(maNguoiDung));
        }

        [HttpPost("{maNguoiDung}")]
        public IActionResult Create(Guid maNguoiDung, [FromBody] TaoCongTyDto dto)
        {
            return _service.TaoCongTy(dto, maNguoiDung)
                ? Ok("Tạo công ty thành công")
                : BadRequest("Tạo thất bại");
        }

        [HttpPut("{maCongTy}")]
        public IActionResult Update(Guid maCongTy, [FromBody] CapNhatCongTyDto dto)
        {
            return _service.CapNhat(maCongTy, dto)
                ? Ok("Cập nhật thành công")
                : BadRequest("Cập nhật thất bại");
        }

        [HttpDelete("{maCongTy}")]
        public IActionResult Delete(Guid maCongTy)
        {
            return _service.Xoa(maCongTy)
                ? Ok("Xóa thành công")
                : BadRequest("Xóa thất bại");
        }
    }
}
