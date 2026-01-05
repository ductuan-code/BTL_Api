using Microsoft.Data.SqlClient;

public class CongTyDAL
{
    private readonly DbHelper _db;

    public CongTyDAL(DbHelper db)
    {
        _db = db;
    }

    public List<object> GetAll()
    {
        var list = new List<object>();
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"SELECT MaCongTy, TenCongTy, Slug, Website, MoTa, Logo, NgayTao
                    FROM CongTy";

        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new
            {
                MaCongTy = reader.GetGuid(0),
                TenCongTy = reader.GetString(1),
                Slug = reader.GetString(2),
                Website = reader.IsDBNull(3) ? null : reader.GetString(3),
                MoTa = reader.IsDBNull(4) ? null : reader.GetString(4),
                Logo = reader.IsDBNull(5) ? null : reader.GetString(5),
                NgayTao = reader.GetDateTime(6)
            });
        }
        return list;
    }

    public void Create(CreateCongTyDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"INSERT INTO CongTy
                    (TenCongTy, Slug, Website, MoTa, Logo, TaoBoi)
                    VALUES
                    (@TenCongTy, @Slug, @Website, @MoTa, @Logo, @TaoBoi)";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TenCongTy", dto.TenCongTy);
        cmd.Parameters.AddWithValue("@Slug", dto.Slug);
        cmd.Parameters.AddWithValue("@Website", (object?)dto.Website ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@MoTa", (object?)dto.MoTa ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Logo", (object?)dto.Logo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TaoBoi", (object?)dto.TaoBoi ?? DBNull.Value);

        cmd.ExecuteNonQuery();
    }

    public void Update(Guid maCongTy, UpdateCongTyDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"UPDATE CongTy
                    SET TenCongTy=@TenCongTy,
                        Website=@Website,
                        MoTa=@MoTa,
                        Logo=@Logo,
                        NgayCapNhat=GETDATE()
                    WHERE MaCongTy=@MaCongTy";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@MaCongTy", maCongTy);
        cmd.Parameters.AddWithValue("@TenCongTy", dto.TenCongTy);
        cmd.Parameters.AddWithValue("@Website", (object?)dto.Website ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@MoTa", (object?)dto.MoTa ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Logo", (object?)dto.Logo ?? DBNull.Value);

        cmd.ExecuteNonQuery();
    }

    public void Delete(Guid maCongTy)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = "DELETE FROM CongTy WHERE MaCongTy=@MaCongTy";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@MaCongTy", maCongTy);
        cmd.ExecuteNonQuery();
    }
}

