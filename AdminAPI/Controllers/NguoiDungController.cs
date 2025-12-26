using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/nguoidung")]
public class NguoiDungController : ControllerBase
{
    private readonly DbHelper _db;

    public NguoiDungController(DbHelper db)
    {
        _db = db;
    }

    // ============================
    // 1. GET: api/nguoidung
    // ============================
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = new List<object>();

        using var conn = _db.GetConnection();
        conn.Open();

        string sql = @"
            SELECT nd.MaNguoiDung, nd.Email, nd.HoTen, nd.SoDienThoai,
                   q.TenQuyen, nd.TrangThai, nd.NgayTao
            FROM NguoiDung nd
            JOIN Quyen q ON nd.MaQuyen = q.MaQuyen
        ";

        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new
            {
                MaNguoiDung = reader["MaNguoiDung"],
                Email = reader["Email"],
                HoTen = reader["HoTen"],
                SoDienThoai = reader["SoDienThoai"],
                Quyen = reader["TenQuyen"],
                TrangThai = reader["TrangThai"],
                NgayTao = reader["NgayTao"]
            });
        }

        return Ok(result);
    }

    // ============================
    // 2. GET: api/nguoidung/{id}
    // ============================
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        string sql = @"
            SELECT nd.MaNguoiDung, nd.Email, nd.HoTen, nd.SoDienThoai,
                   q.TenQuyen, nd.TrangThai
            FROM NguoiDung nd
            JOIN Quyen q ON nd.MaQuyen = q.MaQuyen
            WHERE nd.MaNguoiDung = @Id
        ";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return NotFound("Không tìm thấy người dùng");

        var data = new
        {
            MaNguoiDung = reader["MaNguoiDung"],
            Email = reader["Email"],
            HoTen = reader["HoTen"],
            SoDienThoai = reader["SoDienThoai"],
            Quyen = reader["TenQuyen"],
            TrangThai = reader["TrangThai"]
        };

        return Ok(data);
    }

    // ============================
    // 3. POST: api/nguoidung
    // ============================
    [HttpPost]
    public IActionResult Create([FromBody] CreateNguoiDungDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        string sql = @"
            INSERT INTO NguoiDung (Email, MatKhauHash, HoTen, SoDienThoai, MaQuyen)
            VALUES (@Email, @MatKhauHash, @HoTen, @SoDienThoai, @MaQuyen)
        ";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Email", dto.Email);
        cmd.Parameters.AddWithValue("@MatKhauHash", dto.MatKhauHash);
        cmd.Parameters.AddWithValue("@HoTen", dto.HoTen ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@SoDienThoai", dto.SoDienThoai ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@MaQuyen", dto.MaQuyen);

        cmd.ExecuteNonQuery();

        return Ok("Tạo người dùng thành công");
    }

    // ============================
    // 4. PUT: api/nguoidung/{id}
    // ============================
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateNguoiDungDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        string sql = @"
            UPDATE NguoiDung
            SET HoTen = @HoTen,
                SoDienThoai = @SoDienThoai,
                TrangThai = @TrangThai,
                NgayCapNhat = GETDATE()
            WHERE MaNguoiDung = @Id
        ";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@HoTen", dto.HoTen ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@SoDienThoai", dto.SoDienThoai ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@TrangThai", dto.TrangThai);
        cmd.Parameters.AddWithValue("@Id", id);

        int rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            return NotFound("Không tìm thấy người dùng");

        return Ok("Cập nhật thành công");
    }
}

