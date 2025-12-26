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
            => Ok(_service.GetMyCompanies(maNguoiDung));

        [HttpGet("detail/{maCongTy}")]
        public IActionResult GetDetail(Guid maCongTy)
            => Ok(_service.GetById(maCongTy));

        [HttpPost("{maNguoiDung}")]
        public IActionResult Create(Guid maNguoiDung, [FromBody] TaoCongTyDto dto)
            => Ok(_service.TaoCongTy(dto, maNguoiDung));

        [HttpPut("{maCongTy}")]
        public IActionResult Update(Guid maCongTy, [FromBody] CapNhatCongTyDto dto)
            => Ok(_service.CapNhat(maCongTy, dto));

        [HttpDelete("{maCongTy}")]
        public IActionResult Delete(Guid maCongTy)
            => Ok(_service.Xoa(maCongTy));
    }
}
