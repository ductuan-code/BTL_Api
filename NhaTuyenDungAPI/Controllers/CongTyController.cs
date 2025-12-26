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

        [HttpPost("{maNguoiDung}")]
        public IActionResult Create(Guid maNguoiDung, TaoCongTyDto dto)
        {
            var result = _service.TaoCongTy(dto, maNguoiDung);
            return result ? Ok("Tạo công ty thành công") : BadRequest("Tạo công ty thất bại");
        }
    }
}
