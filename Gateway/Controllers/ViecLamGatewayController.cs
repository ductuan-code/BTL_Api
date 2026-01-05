using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("gateway/vieclam")]
    public class ViecLamGatewayController : ControllerBase
    {
        private readonly HttpClient _http;

        // URL API gốc (AdminAPI)
        private readonly string _adminApi = "https://localhost:7272/api/vieclam";

        public ViecLamGatewayController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        // GET: gateway/vieclam
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _http.GetAsync(_adminApi);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // GET: gateway/vieclam/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _http.GetAsync($"{_adminApi}/{id}");
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // POST: gateway/vieclam
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object body)
        {
            var res = await _http.PostAsJsonAsync(_adminApi, body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // PUT: gateway/vieclam/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] object body)
        {
            var res = await _http.PutAsJsonAsync($"{_adminApi}/{id}", body);
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }

        // DELETE: gateway/vieclam/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _http.DeleteAsync($"{_adminApi}/{id}");
            var data = await res.Content.ReadAsStringAsync();
            return Content(data, "application/json");
        }
    }
}

