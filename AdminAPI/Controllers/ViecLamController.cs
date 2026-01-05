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
    // PUT: api/vieclam/{id}
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateViecLamDto dto)
    {
        _bll.UpdateViecLam(id, dto);
        return Ok("Cập nhật việc làm thành công");
    }

}

