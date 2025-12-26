using DTO.CongTy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL.CongTy
{
    public class CongTyRepository : ICongTyRepository
    {
        private readonly string _conn;

        public CongTyRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Chưa cấu hình ConnectionString");
        }

        public List<CongTyDto> GetByEmployer(Guid maNguoiDung)
        {
            var list = new List<CongTyDto>();

            using SqlConnection conn = new SqlConnection(_conn);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT MaCongTy, TenCongTy, Website, MoTa
                FROM CongTy
                WHERE TaoBoi = @MaNguoiDung";

            cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier).Value = maNguoiDung;

            conn.Open();
            using SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new CongTyDto
                {
                    MaCongTy = rd.GetGuid(0),
                    TenCongTy = rd.GetString(1),
                    Website = rd.IsDBNull(2) ? null : rd.GetString(2),
                    MoTa = rd.IsDBNull(3) ? null : rd.GetString(3)
                });
            }

            return list;
        }

        public CongTyDto? GetById(Guid maCongTy, Guid maNguoiDung)
        {
            using SqlConnection conn = new SqlConnection(_conn);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT MaCongTy, TenCongTy, Website, MoTa
                FROM CongTy
                WHERE MaCongTy = @MaCongTy
                  AND TaoBoi = @MaNguoiDung";

            cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier).Value = maCongTy;
            cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier).Value = maNguoiDung;

            conn.Open();
            using SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                return new CongTyDto
                {
                    MaCongTy = rd.GetGuid(0),
                    TenCongTy = rd.GetString(1),
                    Website = rd.IsDBNull(2) ? null : rd.GetString(2),
                    MoTa = rd.IsDBNull(3) ? null : rd.GetString(3)
                };
            }

            return null;
        }

        public bool Create(TaoCongTyDto dto, Guid maNguoiDung)
        {
            using SqlConnection conn = new SqlConnection(_conn);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO CongTy
                (MaCongTy, TenCongTy, Website, MoTa, TaoBoi, NgayTao, NgayCapNhat)
                VALUES
                (@MaCongTy, @TenCongTy, @Website, @MoTa, @TaoBoi, GETDATE(), GETDATE())";

            cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 255).Value = dto.TenCongTy;
            cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 255).Value = (object?)dto.Website ?? DBNull.Value;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)dto.MoTa ?? DBNull.Value;
            cmd.Parameters.Add("@TaoBoi", SqlDbType.UniqueIdentifier).Value = maNguoiDung;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(Guid maCongTy, TaoCongTyDto dto, Guid maNguoiDung)
        {
            using SqlConnection conn = new SqlConnection(_conn);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE CongTy
                SET TenCongTy = @TenCongTy,
                    Website = @Website,
                    MoTa = @MoTa,
                    NgayCapNhat = GETDATE()
                WHERE MaCongTy = @MaCongTy
                  AND TaoBoi = @MaNguoiDung";

            cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier).Value = maCongTy;
            cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 255).Value = dto.TenCongTy;
            cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 255).Value = (object?)dto.Website ?? DBNull.Value;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)dto.MoTa ?? DBNull.Value;
            cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier).Value = maNguoiDung;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
