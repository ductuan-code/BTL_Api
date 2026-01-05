using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("gateway/congty")]
    public class CongTyGatewayController : ControllerBase
    {
        private readonly HttpClient _http;

        private readonly string _api = "https://localhost:7272/api/congty";

        public CongTyGatewayController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        // GET: gateway/congty
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _http.GetAsync(_api);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // POST: gateway/congty
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object body)
        {
            var res = await _http.PostAsJsonAsync(_api, body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // PUT: gateway/congty/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] object body)
        {
            var res = await _http.PutAsJsonAsync($"{_api}/{id}", body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // DELETE: gateway/congty/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _http.DeleteAsync($"{_api}/{id}");
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }
    }
}
