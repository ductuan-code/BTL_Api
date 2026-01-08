using Microsoft.Data.SqlClient;


public class ThongKeDAL
{
    private readonly DbHelper _db;

    public ThongKeDAL(DbHelper db)
    {
        _db = db;
    }

    private int ExecuteCount(string sql)
    {
        using var conn = _db.GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        return (int)cmd.ExecuteScalar();
    }

    public int TongNguoiDung()
        => ExecuteCount("SELECT COUNT(*) FROM NguoiDung");

    public int TongCongTy()
        => ExecuteCount("SELECT COUNT(*) FROM CongTy");

    public int TongViecLam()
        => ExecuteCount("SELECT COUNT(*) FROM ViecLam");

    public int ViecLamChuaDuyet()
        => ExecuteCount("SELECT COUNT(*) FROM ViecLam WHERE DaDuyet = 0");

    public int TongDonUngTuyen()
        => ExecuteCount("SELECT COUNT(*) FROM DonUngTuyen");

    public object ThongKeTheoNgay()
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var sql = @"
        SELECT 
            CAST(NgayTao AS DATE) AS Ngay,
            COUNT(*) AS SoLuong
        FROM DonUngTuyen
        GROUP BY CAST(NgayTao AS DATE)
        ORDER BY Ngay DESC";

        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        var list = new List<object>();
        while (reader.Read())
        {
            list.Add(new
            {
                Ngay = reader["Ngay"],
                SoLuong = reader["SoLuong"]
            });
        }

        return list;
    }
}
