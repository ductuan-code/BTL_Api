using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Data.SqlClient;

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
                SELECT nd.MaNguoiDung, q.TenQuyen
                FROM NguoiDung nd
                JOIN Quyen q ON nd.MaQuyen = q.MaQuyen
                WHERE nd.Email = @Email AND nd.MatKhauHash = @MatKhau
            ", conn);

            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@MatKhau", dto.MatKhau);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return Unauthorized("Sai email hoặc mật khẩu");

            var maNguoiDung = reader.GetGuid(0);
            var role = reader.GetString(1);

            var jwt = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt["Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, maNguoiDung.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Email, dto.Email)
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

            return Ok(new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireAt = token.ValidTo,
                Role = role,
                MaNguoiDung = maNguoiDung
            });
        }
    }
}
