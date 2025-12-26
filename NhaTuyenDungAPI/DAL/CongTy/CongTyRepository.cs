using DTO.CongTy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL.CongTy
{
    public class CongTyRepository : ICongTyRepository
    {
        private readonly string _connectionString;

        public CongTyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<CongTyDto> GetByEmployer(Guid maNguoiDung)
        {
            var result = new List<CongTyDto>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT MaCongTy, TenCongTy, Website, MoTa
                    FROM CongTy
                    WHERE TaoBoi = @MaNguoiDung";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new CongTyDto
                    {
                        MaCongTy = reader.GetGuid(0),
                        TenCongTy = reader.GetString(1),
                        Website = reader.IsDBNull(2) ? null : reader.GetString(2),
                        MoTa = reader.IsDBNull(3) ? null : reader.GetString(3)
                    });
                }
            }

            return result;
        }

        public bool Create(TaoCongTyDto dto, Guid maNguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    INSERT INTO CongTy
                    (MaCongTy, TenCongTy, Website, MoTa, TaoBoi, NgayTao, NgayCapNhat)
                    VALUES
                    (@MaCongTy, @TenCongTy, @Website, @MoTa, @TaoBoi, GETDATE(), GETDATE())";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaCongTy", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@TenCongTy", dto.TenCongTy);
                cmd.Parameters.AddWithValue("@Website", (object?)dto.Website ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MoTa", (object?)dto.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TaoBoi", maNguoiDung);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
