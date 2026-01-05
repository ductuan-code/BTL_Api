using Microsoft.Data.SqlClient;
using DTO.DonUngTuyen;
using System.Data;

namespace DAL.DonUngTuyen
{
    public class DonUngTuyenRepository : IDonUngTuyenRepository
    {
        private readonly string _connStr;

        public DonUngTuyenRepository(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection");
        }

        public bool TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
INSERT INTO DonUngTuyen
(MaDon, MaViecLam, MaUngVien, MaHoSo, ThuGioiThieu, TrangThai)
VALUES
(@MaDon, @MaViecLam, @MaUngVien, @MaHoSo, @ThuGioiThieu, 'applied')";

            cmd.Parameters.Add("@MaDon", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = dto.MaViecLam;
            cmd.Parameters.Add("@MaUngVien", SqlDbType.UniqueIdentifier).Value = maUngVien;
            cmd.Parameters.Add("@MaHoSo", SqlDbType.UniqueIdentifier)
                .Value = (object?)dto.MaHoSo ?? DBNull.Value;
            cmd.Parameters.Add("@ThuGioiThieu", SqlDbType.NVarChar)
                .Value = (object?)dto.ThuGioiThieu ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<DonUngTuyenDto> LayTheoUngVien(Guid maUngVien)
        {
            var list = new List<DonUngTuyenDto>();

            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
SELECT MaDon, MaViecLam, MaUngVien, MaHoSo, TrangThai, NgayNop
FROM DonUngTuyen
WHERE MaUngVien = @MaUngVien";

            cmd.Parameters.Add("@MaUngVien", SqlDbType.UniqueIdentifier).Value = maUngVien;

            conn.Open();
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new DonUngTuyenDto
                {
                    MaDon = rd.GetGuid(0),
                    MaViecLam = rd.GetGuid(1),
                    MaUngVien = rd.GetGuid(2),
                    MaHoSo = rd.IsDBNull(3) ? null : rd.GetGuid(3),
                    TrangThai = rd.GetString(4),
                    NgayNop = rd.GetDateTime(5)
                });
            }
            return list;
        }

        public List<DonUngTuyenDto> LayTheoViecLam(Guid maViecLam)
        {
            var list = new List<DonUngTuyenDto>();

            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
SELECT MaDon, MaViecLam, MaUngVien, MaHoSo, TrangThai, NgayNop
FROM DonUngTuyen
WHERE MaViecLam = @MaViecLam";

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
                    MaHoSo = rd.IsDBNull(3) ? null : rd.GetGuid(3),
                    TrangThai = rd.GetString(4),
                    NgayNop = rd.GetDateTime(5)
                });
            }
            return list;
        }

        public bool CapNhatTrangThai(Guid maDon, string trangThai)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
UPDATE DonUngTuyen
SET TrangThai = @TrangThai, NgayCapNhat = GETDATE()
WHERE MaDon = @MaDon";

            cmd.Parameters.Add("@MaDon", SqlDbType.UniqueIdentifier).Value = maDon;
            cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = trangThai;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
