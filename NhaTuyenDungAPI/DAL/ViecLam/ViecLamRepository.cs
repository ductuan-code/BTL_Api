using DTO.ViecLam;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NhaTuyenDungAPI.DTO.ViecLam;
using System.Data;

namespace DAL.ViecLam
{
    public class ViecLamRepository : IViecLamRepository
    {
        private readonly string _connectionString;

        public ViecLamRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private string TaoSlug(string text)
        {
            return text.ToLower().Replace(" ", "-");
        }

        public bool Create(TaoViecLamDto dto, Guid maNguoiDung)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
INSERT INTO ViecLam
(MaViecLam, MaCongTy, TaoBoi, TieuDe, Slug, DiaDiem, LoaiHinhCongViec,
 MoTa, YeuCau, TrachNhiem, LuongToiThieu, LuongToiDa,
 DaDuyet, DuocHienThi, NgayDang, NgayHetHan)
VALUES
(@MaViecLam, @MaCongTy, @TaoBoi, @TieuDe, @Slug, @DiaDiem, @LoaiHinh,
 @MoTa, @YeuCau, @TrachNhiem, @LuongMin, @LuongMax,
 0, 0, GETDATE(), @NgayHetHan)";

            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier).Value = dto.MaCongTy;
            cmd.Parameters.Add("@TaoBoi", SqlDbType.UniqueIdentifier).Value = maNguoiDung;
            cmd.Parameters.Add("@TieuDe", SqlDbType.NVarChar, 255).Value = dto.TieuDe;
            cmd.Parameters.Add("@Slug", SqlDbType.NVarChar, 255).Value = TaoSlug(dto.TieuDe);
            cmd.Parameters.Add("@DiaDiem", SqlDbType.NVarChar, 255).Value = (object?)dto.DiaDiem ?? DBNull.Value;
            cmd.Parameters.Add("@LoaiHinh", SqlDbType.NVarChar, 50).Value = (object?)dto.LoaiHinhCongViec ?? DBNull.Value;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)dto.MoTa ?? DBNull.Value;
            cmd.Parameters.Add("@YeuCau", SqlDbType.NVarChar).Value = (object?)dto.YeuCau ?? DBNull.Value;
            cmd.Parameters.Add("@TrachNhiem", SqlDbType.NVarChar).Value = (object?)dto.TrachNhiem ?? DBNull.Value;
            cmd.Parameters.Add("@LuongMin", SqlDbType.Int).Value = (object?)dto.LuongToiThieu ?? DBNull.Value;
            cmd.Parameters.Add("@LuongMax", SqlDbType.Int).Value = (object?)dto.LuongToiDa ?? DBNull.Value;
            cmd.Parameters.Add("@NgayHetHan", SqlDbType.DateTime).Value = (object?)dto.NgayHetHan ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<ViecLamDto> GetByEmployer(Guid maNguoiDung)
        {
            var list = new List<ViecLamDto>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
SELECT MaViecLam, TieuDe, Slug, DiaDiem, LoaiHinhCongViec,
       LuongToiThieu, LuongToiDa, DaDuyet, DuocHienThi, NgayDang
FROM ViecLam
WHERE TaoBoi = @MaNguoiDung";

            cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier).Value = maNguoiDung;

            conn.Open();
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new ViecLamDto
                {
                    MaViecLam = rd.GetGuid(0),
                    TieuDe = rd.GetString(1),
                    Slug = rd.GetString(2),
                    DiaDiem = rd.IsDBNull(3) ? null : rd.GetString(3),
                    LoaiHinhCongViec = rd.IsDBNull(4) ? null : rd.GetString(4),
                    LuongToiThieu = rd.IsDBNull(5) ? null : rd.GetInt32(5),
                    LuongToiDa = rd.IsDBNull(6) ? null : rd.GetInt32(6),
                    DaDuyet = rd.GetBoolean(7),
                    DuocHienThi = rd.GetBoolean(8),
                    NgayDang = rd.GetDateTime(9)
                });
            }

            return list;
        }

        public ViecLamDto? GetDetail(Guid maViecLam)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT MaViecLam, TieuDe, Slug, DiaDiem, LoaiHinhCongViec,
                                LuongToiThieu, LuongToiDa, DaDuyet, DuocHienThi, NgayDang
                                FROM ViecLam WHERE MaViecLam = @MaViecLam";

            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = maViecLam;

            conn.Open();
            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new ViecLamDto
            {
                MaViecLam = rd.GetGuid(0),
                TieuDe = rd.GetString(1),
                Slug = rd.GetString(2),
                DiaDiem = rd.IsDBNull(3) ? null : rd.GetString(3),
                LoaiHinhCongViec = rd.IsDBNull(4) ? null : rd.GetString(4),
                LuongToiThieu = rd.IsDBNull(5) ? null : rd.GetInt32(5),
                LuongToiDa = rd.IsDBNull(6) ? null : rd.GetInt32(6),
                DaDuyet = rd.GetBoolean(7),
                DuocHienThi = rd.GetBoolean(8),
                NgayDang = rd.GetDateTime(9)
            };
        }

        public bool Update(Guid maViecLam, CapNhatViecLamDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
UPDATE ViecLam
SET TieuDe=@TieuDe,
    Slug=@Slug,
    DiaDiem=@DiaDiem,
    LoaiHinhCongViec=@LoaiHinh,
    MoTa=@MoTa,
    YeuCau=@YeuCau,
    TrachNhiem=@TrachNhiem,
    LuongToiThieu=@LuongMin,
    LuongToiDa=@LuongMax,
    NgayHetHan=@NgayHetHan,
    DaDuyet=0
WHERE MaViecLam=@MaViecLam";

            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = maViecLam;
            cmd.Parameters.Add("@TieuDe", SqlDbType.NVarChar, 255).Value = dto.TieuDe;
            cmd.Parameters.Add("@Slug", SqlDbType.NVarChar, 255).Value = TaoSlug(dto.TieuDe);
            cmd.Parameters.Add("@DiaDiem", SqlDbType.NVarChar, 255).Value = (object?)dto.DiaDiem ?? DBNull.Value;
            cmd.Parameters.Add("@LoaiHinh", SqlDbType.NVarChar, 50).Value = (object?)dto.LoaiHinhCongViec ?? DBNull.Value;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)dto.MoTa ?? DBNull.Value;
            cmd.Parameters.Add("@YeuCau", SqlDbType.NVarChar).Value = (object?)dto.YeuCau ?? DBNull.Value;
            cmd.Parameters.Add("@TrachNhiem", SqlDbType.NVarChar).Value = (object?)dto.TrachNhiem ?? DBNull.Value;
            cmd.Parameters.Add("@LuongMin", SqlDbType.Int).Value = (object?)dto.LuongToiThieu ?? DBNull.Value;
            cmd.Parameters.Add("@LuongMax", SqlDbType.Int).Value = (object?)dto.LuongToiDa ?? DBNull.Value;
            cmd.Parameters.Add("@NgayHetHan", SqlDbType.DateTime).Value = (object?)dto.NgayHetHan ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Toggle(Guid maViecLam)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE ViecLam SET DuocHienThi = ~DuocHienThi WHERE MaViecLam=@MaViecLam";
            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = maViecLam;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(Guid maViecLam)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM ViecLam WHERE MaViecLam=@MaViecLam";
            cmd.Parameters.Add("@MaViecLam", SqlDbType.UniqueIdentifier).Value = maViecLam;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
