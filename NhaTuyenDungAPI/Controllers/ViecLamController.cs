using BLL.ViecLam;
using DTO.ViecLam;
using Microsoft.AspNetCore.Mvc;
using NhaTuyenDungAPI.DTO.ViecLam;

namespace NhaTuyenDungAPI.Controllers
{
    [ApiController]
    [Route("api/employer/jobs")]
    public class ViecLamController : ControllerBase
    {
        private readonly IViecLamService _service;

        public ViecLamController(IViecLamService service)
        {
            _service = service;
        }

        [HttpPost("{maNguoiDung}")]
        public IActionResult Create(Guid maNguoiDung, TaoViecLamDto dto)
            => Ok(_service.TaoViecLam(dto, maNguoiDung));

        [HttpGet("{maNguoiDung}")]
        public IActionResult GetMyJobs(Guid maNguoiDung)
            => Ok(_service.LayDanhSach(maNguoiDung));

        [HttpGet("detail/{maViecLam}")]
        public IActionResult Detail(Guid maViecLam)
            => Ok(_service.ChiTiet(maViecLam));

        [HttpPut("{maViecLam}")]
        public IActionResult Update(Guid maViecLam, CapNhatViecLamDto dto)
            => Ok(_service.CapNhat(maViecLam, dto));

        [HttpPut("{maViecLam}/toggle")]
        public IActionResult Toggle(Guid maViecLam)
            => Ok(_service.AnHien(maViecLam));

        [HttpDelete("{maViecLam}")]
        public IActionResult Delete(Guid maViecLam)
            => Ok(_service.Xoa(maViecLam));
    }
}
