using Microsoft.Data.SqlClient;

public class DonUngTuyenDAL
{
    private readonly DbHelper _db;

    public DonUngTuyenDAL(DbHelper db)
    {
        _db = db;
    }

    public List<object> GetAll()
    {
        var list = new List<object>();
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(
            @"SELECT * FROM DonUngTuyen ORDER BY NgayNop DESC", conn);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new
            {
                MaDon = reader["MaDon"],
                MaViecLam = reader["MaViecLam"],
                MaUngVien = reader["MaUngVien"],
                TrangThai = reader["TrangThai"],
                NgayNop = reader["NgayNop"]
            });
        }
        return list;
    }

    public void Create(CreateDonUngTuyenDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"
INSERT INTO DonUngTuyen
(MaViecLam, MaUngVien, MaHoSo, ThuGioiThieu)
VALUES (@MaViecLam, @MaUngVien, @MaHoSo, @ThuGioiThieu)";

        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@MaViecLam", dto.MaViecLam);
        cmd.Parameters.AddWithValue("@MaUngVien", dto.MaUngVien);
        cmd.Parameters.AddWithValue("@MaHoSo",dto.MaHoSo.HasValue ? dto.MaHoSo : DBNull.Value);
        cmd.Parameters.AddWithValue("@ThuGioiThieu", dto.ThuGioiThieu ?? "");

        cmd.ExecuteNonQuery();
    }

    public void UpdateTrangThai(Guid maDon, string trangThai)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(
            @"UPDATE DonUngTuyen
              SET TrangThai=@TrangThai, NgayCapNhat=GETDATE()
              WHERE MaDon=@MaDon", conn);

        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
        cmd.Parameters.AddWithValue("@MaDon", maDon);

        cmd.ExecuteNonQuery();
    }
}

