using DTO.DonUngTuyen;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL.DonUngTuyen
{
    public class DonUngTuyenRepository : IDonUngTuyenRepository
    {
        private readonly string _connectionString;

        public DonUngTuyenRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public bool TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO DonUngTuyen
                (MaDon, MaViecLam, MaUngVien, TrangThai, NgayNop, GhiChu)
                VALUES
                (@MaDon, @MaViecLam, @MaUngVien, N'Chờ duyệt', GETDATE(), @GhiChu)
            ";

            cmd.Parameters.Add("@MaDon", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = dto.MaViecLam;
            cmd.Parameters.Add("@MaUngVien", SqlDbType.UniqueIdentifier).Value = maUngVien;
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
                (object?)dto.GhiChu ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<DonUngTuyenDto> LayTheoViecLam(Guid maViecLam)
        {
            var list = new List<DonUngTuyenDto>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT MaDon, MaViecLam, MaUngVien, TrangThai, NgayNop, GhiChu
                FROM DonUngTuyen
                WHERE MaViecLam = @MaViecLam
            ";

            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = maViecLam;
            conn.Open();

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new DonUngTuyenDto
                {
                    MaDon = rd.GetGuid(0),
                    MaViecLam = rd.GetGuid(1),
                    MaUngVien = rd.GetGuid(2),
                    TrangThai = rd.GetString(3),
                    NgayNop = rd.GetDateTime(4),
                    GhiChu = rd.IsDBNull(5) ? null : rd.GetString(5)
                });
            }

            return list;
        }

        public DonUngTuyenDto? LayChiTiet(Guid maDon)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT MaDon, MaViecLam, MaUngVien, TrangThai, NgayNop, GhiChu
                FROM DonUngTuyen
                WHERE MaDon = @MaDon
            ";

            cmd.Parameters.Add("@MaDon", SqlDbType.UniqueIdentifier).Value = maDon;
            conn.Open();

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new DonUngTuyenDto
            {
                MaDon = rd.GetGuid(0),
                MaViecLam = rd.GetGuid(1),
                MaUngVien = rd.GetGuid(2),
                TrangThai = rd.GetString(3),
                NgayNop = rd.GetDateTime(4),
                GhiChu = rd.IsDBNull(5) ? null : rd.GetString(5)
            };
        }

        public bool CapNhatTrangThai(Guid maDon, string trangThai)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE DonUngTuyen
                SET TrangThai = @TrangThai
                WHERE MaDon = @MaDon
            ";

            cmd.Parameters.Add("@MaDon", SqlDbType.UniqueIdentifier).Value = maDon;
            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = trangThai;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
