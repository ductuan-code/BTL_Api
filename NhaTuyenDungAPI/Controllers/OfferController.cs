using BLL.Offer;
using DTO.Offer;
using Microsoft.AspNetCore.Mvc;

namespace NhaTuyenDungAPI.Controllers
{
    [ApiController]
    [Route("api/employer/offers")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _service;

        public OfferController(IOfferService service)
        {
            _service = service;
        }

        [HttpPost("{maNguoiGui}")]
        public IActionResult Tao(Guid maNguoiGui, TaoOfferDto dto)
            => Ok(_service.TaoOffer(dto, maNguoiGui));

        [HttpGet("don/{maDon}")]
        public IActionResult GetByDon(Guid maDon)
            => Ok(_service.GetByDon(maDon));

        [HttpPut("{maOffer}/trang-thai")]
        public IActionResult CapNhatTrangThai(Guid maOffer, string trangThai)
            => Ok(_service.CapNhatTrangThai(maOffer, trangThai));
    }
}
