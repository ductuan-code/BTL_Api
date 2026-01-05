using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("gateway/donungtuyen")]
    public class DonUngTuyenGatewayController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly string _adminApi = "https://localhost:7272/api/donungtuyen";

        public DonUngTuyenGatewayController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        // GET: gateway/donungtuyen
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _http.GetAsync(_adminApi);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // POST: gateway/donungtuyen
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object body)
        {
            var res = await _http.PostAsJsonAsync(_adminApi, body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // PUT: gateway/donungtuyen/{id}/trangthai
        [HttpPut("{id}/trangthai")]
        public async Task<IActionResult> UpdateTrangThai(Guid id, [FromBody] object body)
        {
            var res = await _http.PutAsJsonAsync($"{_adminApi}/{id}/trangthai", body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }
    }
}