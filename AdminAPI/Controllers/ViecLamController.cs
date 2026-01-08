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
    // PUT: api/vieclam/{id}/duyet
    [HttpPut("{id}/duyet")]
    public IActionResult DuyetViecLam(Guid id)
    {
        _bll.DuyetViecLam(id);
        return Ok("Duyệt việc làm thành công");
    }
    // PUT: api/vieclam/{id}/hienthi
    [HttpPut("{id}/hienthi")]
    public IActionResult HienThi(Guid id)
    {
        _bll.AnHienViecLam(id, true);
        return Ok("Đã hiển thị việc làm");
    }

    // PUT: api/vieclam/{id}/an
    [HttpPut("{id}/an")]
    public IActionResult An(Guid id)
    {
        _bll.AnHienViecLam(id, false);
        return Ok("Đã ẩn việc làm");
    }
    [HttpGet("chua-duyet")]
    public IActionResult GetViecLamChuaDuyet()
    {
        return Ok(_bll.GetViecLamChuaDuyet());
    }
    // GET: api/vieclam/search?keyword=backend
    [HttpGet("search")]
    public IActionResult Search(string keyword)
    {
        var result = _bll.Search(keyword);
        return Ok(result);
    }
    // GET: api/vieclam/filter/diadiem?value=Hà Nội
    [HttpGet("filter/diadiem")]
    public IActionResult FilterByDiaDiem(string value)
    {
        return Ok(_bll.FilterByDiaDiem(value));
    }
    // GET: api/vieclam/filter/luong?min=1000&max=3000
    [HttpGet("filter/luong")]
    public IActionResult FilterByLuong(int min, int max)
    {
        return Ok(_bll.FilterByLuong(min, max));
    }






}

