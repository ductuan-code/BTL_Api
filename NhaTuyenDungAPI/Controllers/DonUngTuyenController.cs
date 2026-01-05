using BLL.DonUngTuyen;
using DTO.DonUngTuyen;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/applications")]
public class DonUngTuyenController : ControllerBase
{
    private readonly IDonUngTuyenService _service;

    public DonUngTuyenController(IDonUngTuyenService service)
    {
        _service = service;
    }

    [HttpPost("{maUngVien}")]
    public IActionResult TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto)
        => Ok(_service.TaoDon(maUngVien, dto));

    [HttpGet("ungvien/{maUngVien}")]
    public IActionResult LayTheoUngVien(Guid maUngVien)
        => Ok(_service.LayTheoUngVien(maUngVien));

    [HttpGet("vieclam/{maViecLam}")]
    public IActionResult LayTheoViecLam(Guid maViecLam)
        => Ok(_service.LayTheoViecLam(maViecLam));

    [HttpPut("{maDon}/trangthai")]
    public IActionResult CapNhatTrangThai(Guid maDon, string trangThai)
        => Ok(_service.CapNhatTrangThai(maDon, trangThai));
}
