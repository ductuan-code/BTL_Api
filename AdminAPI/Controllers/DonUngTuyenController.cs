using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/donungtuyen")]
public class DonUngTuyenController : ControllerBase
{
    private readonly DonUngTuyenBLL _bll;

    public DonUngTuyenController(DonUngTuyenBLL bll)
    {
        _bll = bll;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bll.GetAll());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateDonUngTuyenDto dto)
    {
        _bll.Create(dto);
        return Ok("Ứng tuyển thành công");
    }

    [HttpPut("{id}/trangthai")]
    public IActionResult UpdateTrangThai(Guid id, [FromBody] UpdateTrangThaiDonDto dto)
    {
        _bll.UpdateTrangThai(id, dto.TrangThai);
        return Ok("Cập nhật trạng thái thành công");
    }
}

