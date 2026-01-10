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





}

