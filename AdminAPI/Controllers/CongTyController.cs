using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/congty")]
public class CongTyController : ControllerBase
{
    private readonly CongTyBLL _bll;

    public CongTyController(CongTyBLL bll)
    {
        _bll = bll;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bll.GetAll());
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateCongTyDto dto)
    {
        _bll.Create(dto);
        return Ok("Tạo công ty thành công");
    }
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateCongTyDto dto)
    {
        _bll.Update(id, dto);
        return Ok("Cập nhật công ty thành công");
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _bll.Delete(id);
        return Ok("Xóa công ty thành công");
    }
}
