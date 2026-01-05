using Microsoft.Data.SqlClient;

public class ViecLamDAL
{
    private readonly DbHelper _db;

    public ViecLamDAL(DbHelper db)
    {
        _db = db;
    }

    public List<object> GetAll()
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand("SELECT * FROM ViecLam", conn);
        var reader = cmd.ExecuteReader();

        var list = new List<object>();
        while (reader.Read())
        {
            list.Add(new
            {
                MaViecLam = reader["MaViecLam"],
                TieuDe = reader["TieuDe"],
                DiaDiem = reader["DiaDiem"],
                LuongToiThieu = reader["LuongToiThieu"],
                LuongToiDa = reader["LuongToiDa"],
                DaDuyet = reader["DaDuyet"]
            });
        }
        return list;
    }

    public void Create(CreateViecLamDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"
INSERT INTO ViecLam
(MaCongTy, TaoBoi, TieuDe, Slug, DiaDiem, LoaiHinhCongViec,
 MoTa, YeuCau, TrachNhiem, LuongToiThieu, LuongToiDa, DaDuyet, DuocHienThi)
VALUES
(@MaCongTy, @TaoBoi, @TieuDe, @Slug, @DiaDiem, @LoaiHinh,
 @MoTa, @YeuCau, @TrachNhiem, @LuongMin, @LuongMax, 0, 0)";

        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@MaCongTy", dto.MaCongTy);
        cmd.Parameters.AddWithValue("@TaoBoi", (object?)dto.TaoBoi ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TieuDe", dto.TieuDe);
        cmd.Parameters.AddWithValue("@Slug", dto.Slug);
        cmd.Parameters.AddWithValue("@DiaDiem", (object?)dto.DiaDiem ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@LoaiHinh", (object?)dto.LoaiHinhCongViec ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@MoTa", (object?)dto.MoTa ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@YeuCau", (object?)dto.YeuCau ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TrachNhiem", (object?)dto.TrachNhiem ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@LuongMin", (object?)dto.LuongToiThieu ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@LuongMax", (object?)dto.LuongToiDa ?? DBNull.Value);

        cmd.ExecuteNonQuery();
    }
    public void Update(Guid id, UpdateViecLamDto dto)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"
        UPDATE ViecLam SET
            TieuDe = @TieuDe,
            DiaDiem = @DiaDiem,
            LoaiHinhCongViec = @LoaiHinhCongViec,
            MoTa = @MoTa,
            YeuCau = @YeuCau,
            TrachNhiem = @TrachNhiem,
            LuongToiThieu = @LuongToiThieu,
            LuongToiDa = @LuongToiDa,
            NgayHetHan = @NgayHetHan,
            DuocHienThi = @DuocHienThi,
            NgayCapNhat = GETDATE()
        WHERE MaViecLam = @Id
    ";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@TieuDe", dto.TieuDe ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@DiaDiem", dto.DiaDiem ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@LoaiHinhCongViec", dto.LoaiHinhCongViec ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@MoTa", dto.MoTa ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@YeuCau", dto.YeuCau ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@TrachNhiem", dto.TrachNhiem ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@LuongToiThieu", dto.LuongToiThieu ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@LuongToiDa", dto.LuongToiDa ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@NgayHetHan", dto.NgayHetHan ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@DuocHienThi", dto.DuocHienThi);

        cmd.ExecuteNonQuery();
    }

}
