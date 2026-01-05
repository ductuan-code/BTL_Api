using Microsoft.Data.SqlClient;

public class NguoiDungDAL
{
    private readonly DbHelper _db;

    public NguoiDungDAL(DbHelper db)
    {
        _db = db;
    }

    public List<object> GetAll()
    {
        var result = new List<object>();

        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"
            SELECT nd.MaNguoiDung, nd.Email, nd.HoTen, nd.SoDienThoai,
                   q.TenQuyen, nd.TrangThai
            FROM NguoiDung nd
            JOIN Quyen q ON nd.MaQuyen = q.MaQuyen";

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
                TrangThai = reader["TrangThai"]
            });
        }

        return result;
    }

    public void Create(CreateNguoiDungDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"
            INSERT INTO NguoiDung (Email, MatKhauHash, HoTen, SoDienThoai, MaQuyen)
            VALUES (@Email, @MatKhauHash, @HoTen, @SoDienThoai, @MaQuyen)";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Email", dto.Email);
        cmd.Parameters.AddWithValue("@MatKhauHash", dto.MatKhauHash);
        cmd.Parameters.AddWithValue("@HoTen", dto.HoTen ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@SoDienThoai", dto.SoDienThoai ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@MaQuyen", dto.MaQuyen);

        cmd.ExecuteNonQuery();
    }
    public void Update(Guid id, UpdateNguoiDungDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"
        UPDATE NguoiDung
        SET HoTen = @HoTen,
            SoDienThoai = @SoDienThoai,
            TrangThai = @TrangThai,
            NgayCapNhat = GETDATE()
        WHERE MaNguoiDung = @Id";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@HoTen", dto.HoTen ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@SoDienThoai", dto.SoDienThoai ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@TrangThai", dto.TrangThai);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
    public void Delete(Guid id)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = "DELETE FROM NguoiDung WHERE MaNguoiDung = @Id";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }


}

