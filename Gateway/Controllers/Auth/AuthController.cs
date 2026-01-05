using Gateway.DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gateway.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto dto)
        {
            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));

            conn.Open();

            var cmd = new SqlCommand(@"
                SELECT nd.MaNguoiDung, nd.Email, q.TenQuyen
                FROM NguoiDung nd
                JOIN Quyen q ON nd.MaQuyen = q.MaQuyen
                WHERE nd.Email = @Email
                  AND nd.MatKhauHash = @MatKhau
                  AND nd.TrangThai = 1
            ", conn);

            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@MatKhau", dto.MatKhau);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return Unauthorized("Sai email hoặc mật khẩu");

            var user = new LoginResponseDto
            {
                MaNguoiDung = reader.GetGuid(0),
                Email = reader.GetString(1),
                TenQuyen = reader.GetString(2)
            };

            // 🔐 TẠO TOKEN
            var jwt = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt["Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.MaNguoiDung.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.TenQuyen)
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(jwt["ExpireMinutes"])),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = user.TenQuyen
            });
        }
    }
}
