using Microsoft.Data.SqlClient;

public class AuthRepository : IAuthRepository
{
    private readonly string _conn;

    public AuthRepository(IConfiguration config)
    {
        _conn = config.GetConnectionString("DefaultConnection");
    }

    public LoginResponseDto Login(string email, string matKhau)
    {
        using var conn = new SqlConnection(_conn);
        using var cmd = conn.CreateCommand();

        cmd.CommandText = @"
            SELECT nd.MaNguoiDung, nd.Email, q.TenQuyen
            FROM NguoiDung nd
            JOIN Quyen q ON nd.MaQuyen = q.MaQuyen
            WHERE nd.Email = @Email
              AND nd.MatKhauHash = @MatKhau
              AND nd.TrangThai = 1
        ";

        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@MatKhau", matKhau);

        conn.Open();
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;

        return new LoginResponseDto
        {
            MaNguoiDung = r.GetGuid(0),
            Email = r.GetString(1),
            TenQuyen = r.GetString(2)
        };
    }
}
