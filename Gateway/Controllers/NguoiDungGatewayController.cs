using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("gateway/nguoidung")]
    public class NguoiDungGatewayController : ControllerBase
    {
        private readonly HttpClient _http;

        // ĐÚNG port AdminAPI của bạn
        private readonly string _adminApi = "https://localhost:7272/api/nguoidung";

        public NguoiDungGatewayController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        // GET: gateway/nguoidung
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _http.GetAsync(_adminApi);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // POST: gateway/nguoidung
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object body)
        {
            var res = await _http.PostAsJsonAsync(_adminApi, body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // PUT: gateway/nguoidung/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] object body)
        {
            var res = await _http.PutAsJsonAsync($"{_adminApi}/{id}", body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // DELETE: gateway/nguoidung/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _http.DeleteAsync($"{_adminApi}/{id}");
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }
    }
}
