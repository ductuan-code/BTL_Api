using BLL.PhongVan;
using DTO.PhongVan;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/employer/phong-van")]
public class PhongVanController : ControllerBase
{
    private readonly IPhongVanService _service;

    public PhongVanController(IPhongVanService service)
    {
        _service = service;
    }

    [HttpPost("{maNguoiDung}")]
    public IActionResult Tao(Guid maNguoiDung, TaoPhongVanDto dto)
        => Ok(_service.Tao(maNguoiDung, dto));

    [HttpGet("don/{maDon}")]
    public IActionResult LayTheoDon(Guid maDon)
        => Ok(_service.LayTheoDon(maDon));

    [HttpGet("{maPhongVan}")]
    public IActionResult ChiTiet(Guid maPhongVan)
        => Ok(_service.LayChiTiet(maPhongVan));

    [HttpPut("{maPhongVan}")]
    public IActionResult CapNhat(Guid maPhongVan, CapNhatPhongVanDto dto)
        => Ok(_service.CapNhat(maPhongVan, dto));

    [HttpPut("{maPhongVan}/trang-thai")]
    public IActionResult CapNhatTrangThai(Guid maPhongVan, string trangThai)
        => Ok(_service.CapNhatTrangThai(maPhongVan, trangThai));

    [HttpGet("employer/{maNguoiDung}")]
    public IActionResult LayTheoNTD(Guid maNguoiDung)
        => Ok(_service.LayTheoNhaTuyenDung(maNguoiDung));
}
