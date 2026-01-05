using System.Data;
using Microsoft.Data.SqlClient;
using DTO.PhongVan;

namespace DAL.PhongVan
{
    public class PhongVanRepository : IPhongVanRepository
    {
        private readonly string _connectionString;

        public PhongVanRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public bool Tao(Guid maNguoiDung, TaoPhongVanDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO PhongVan
                (MaPhongVan, MaDon, MaNguoiPhongVan, ThoiGian, ThoiLuong, DiaDiem, GhiChu)
                VALUES
                (@MaPhongVan, @MaDon, @MaNguoiPhongVan, @ThoiGian, @ThoiLuong, @DiaDiem, @GhiChu)
            ";

            cmd.Parameters.Add("@MaPhongVan", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@MaDon", SqlDbType.UniqueIdentifier).Value = dto.MaDon;
            cmd.Parameters.Add("@MaNguoiPhongVan", SqlDbType.UniqueIdentifier).Value = maNguoiDung;
            cmd.Parameters.Add("@ThoiGian", SqlDbType.DateTime).Value = dto.ThoiGian;
            cmd.Parameters.Add("@ThoiLuong", SqlDbType.Int).Value = (object?)dto.ThoiLuong ?? DBNull.Value;
            cmd.Parameters.Add("@DiaDiem", SqlDbType.NVarChar).Value = (object?)dto.DiaDiem ?? DBNull.Value;
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)dto.GhiChu ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<PhongVanDto> LayTheoDon(Guid maDon)
        {
            var list = new List<PhongVanDto>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT MaPhongVan, MaDon, ThoiGian, ThoiLuong, DiaDiem, TrangThai, GhiChu
                FROM PhongVan
                WHERE MaDon = @MaDon
                ORDER BY ThoiGian
            ";

            cmd.Parameters.Add("@MaDon", SqlDbType.UniqueIdentifier).Value = maDon;
            conn.Open();

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new PhongVanDto
                {
                    MaPhongVan = rd.GetGuid(0),
                    MaDon = rd.GetGuid(1),
                    ThoiGian = rd.GetDateTime(2),
                    ThoiLuong = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                    DiaDiem = rd.IsDBNull(4) ? null : rd.GetString(4),
                    TrangThai = rd.GetString(5),
                    GhiChu = rd.IsDBNull(6) ? null : rd.GetString(6)
                });
            }
            return list;
        }

        public PhongVanDto? LayChiTiet(Guid maPhongVan)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT MaPhongVan, MaDon, ThoiGian, ThoiLuong, DiaDiem, TrangThai, GhiChu
                FROM PhongVan
                WHERE MaPhongVan = @MaPhongVan
            ";

            cmd.Parameters.Add("@MaPhongVan", SqlDbType.UniqueIdentifier).Value = maPhongVan;
            conn.Open();

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new PhongVanDto
            {
                MaPhongVan = rd.GetGuid(0),
                MaDon = rd.GetGuid(1),
                ThoiGian = rd.GetDateTime(2),
                ThoiLuong = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                DiaDiem = rd.IsDBNull(4) ? null : rd.GetString(4),
                TrangThai = rd.GetString(5),
                GhiChu = rd.IsDBNull(6) ? null : rd.GetString(6)
            };
        }

        public bool CapNhat(Guid maPhongVan, CapNhatPhongVanDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE PhongVan
                SET ThoiGian=@ThoiGian,
                    ThoiLuong=@ThoiLuong,
                    DiaDiem=@DiaDiem,
                    GhiChu=@GhiChu,
                    NgayCapNhat=GETDATE()
                WHERE MaPhongVan=@MaPhongVan
            ";

            cmd.Parameters.Add("@MaPhongVan", SqlDbType.UniqueIdentifier).Value = maPhongVan;
            cmd.Parameters.Add("@ThoiGian", SqlDbType.DateTime).Value = dto.ThoiGian;
            cmd.Parameters.Add("@ThoiLuong", SqlDbType.Int).Value = (object?)dto.ThoiLuong ?? DBNull.Value;
            cmd.Parameters.Add("@DiaDiem", SqlDbType.NVarChar).Value = (object?)dto.DiaDiem ?? DBNull.Value;
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)dto.GhiChu ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool CapNhatTrangThai(Guid maPhongVan, string trangThai)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE PhongVan
                SET TrangThai=@TrangThai, NgayCapNhat=GETDATE()
                WHERE MaPhongVan=@MaPhongVan
            ";

            cmd.Parameters.Add("@MaPhongVan", SqlDbType.UniqueIdentifier).Value = maPhongVan;
            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = trangThai;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<PhongVanDto> LayTheoNhaTuyenDung(Guid maNguoiDung)
        {
            var list = new List<PhongVanDto>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT MaPhongVan, MaDon, ThoiGian, ThoiLuong, DiaDiem, TrangThai, GhiChu
                FROM PhongVan
                WHERE MaNguoiPhongVan=@MaNguoiDung
                ORDER BY ThoiGian DESC
            ";

            cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier).Value = maNguoiDung;
            conn.Open();

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new PhongVanDto
                {
                    MaPhongVan = rd.GetGuid(0),
                    MaDon = rd.GetGuid(1),
                    ThoiGian = rd.GetDateTime(2),
                    ThoiLuong = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                    DiaDiem = rd.IsDBNull(4) ? null : rd.GetString(4),
                    TrangThai = rd.GetString(5),
                    GhiChu = rd.IsDBNull(6) ? null : rd.GetString(6)
                });
            }
            return list;
        }
    }
}
