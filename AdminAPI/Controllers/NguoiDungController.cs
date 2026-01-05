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
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateNguoiDungDto dto)
    {
        _bll.UpdateNguoiDung(id, dto);
        return Ok("Cập nhật thành công");
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _bll.DeleteNguoiDung(id);
        return Ok("Xoá thành công");
    }


}


