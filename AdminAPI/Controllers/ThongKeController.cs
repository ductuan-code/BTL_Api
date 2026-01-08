using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/thongke")]
public class ThongKeController : ControllerBase
{
    private readonly ThongKeBLL _bll;

    public ThongKeController(ThongKeBLL bll)
    {
        _bll = bll;
    }

    [HttpGet("tong-nguoidung")]
    public IActionResult TongNguoiDung()
        => Ok(_bll.TongNguoiDung());

    [HttpGet("tong-congty")]
    public IActionResult TongCongTy()
        => Ok(_bll.TongCongTy());

    [HttpGet("tong-vieclam")]
    public IActionResult TongViecLam()
        => Ok(_bll.TongViecLam());

    [HttpGet("vieclam-chuaduyet")]
    public IActionResult ViecLamChuaDuyet()
        => Ok(_bll.ViecLamChuaDuyet());

    [HttpGet("tong-donungtuyen")]
    public IActionResult TongDonUngTuyen()
        => Ok(_bll.TongDonUngTuyen());

    [HttpGet("donungtuyen-theongay")]
    public IActionResult DonUngTuyenTheoNgay()
        => Ok(_bll.ThongKeTheoNgay());
}
