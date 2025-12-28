using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/nguoidung")]
public class NguoiDungController : ControllerBase
{
    private readonly NguoiDungBLL _bll;

    public NguoiDungController(NguoiDungBLL bll)
    {
        _bll = bll;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bll.GetAllNguoiDung());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateNguoiDungDto dto)
    {
        _bll.CreateNguoiDung(dto);
        return Ok("Tạo người dùng thành công");
    }
}


