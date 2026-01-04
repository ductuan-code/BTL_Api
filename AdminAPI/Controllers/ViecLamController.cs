using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/vieclam")]
public class ViecLamController : ControllerBase
{
    private readonly ViecLamBLL _bll;

    public ViecLamController(ViecLamBLL bll)
    {
        _bll = bll;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bll.GetAll());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateViecLamDto dto)
    {
        _bll.Create(dto);
        return Ok("Tạo việc làm thành công");
    }
}

