using DTO.CongTy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.CongTy
{
    public class CongTyRepository : ICongTyRepository
    {
        private readonly string _connectionString;

        public CongTyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Chưa cấu hình ConnectionString DefaultConnection");
        }

        // =========================
        // 1. LẤY DANH SÁCH CÔNG TY
        // =========================
        public List<CongTyDto> GetByEmployer(Guid maNguoiDung)
        {
            var result = new List<CongTyDto>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT MaCongTy, TenCongTy, Website, MoTa
                    FROM CongTy
                    WHERE TaoBoi = @MaNguoiDung";

                cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier)
                              .Value = maNguoiDung;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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
            }

            return result;
        }

        // =========================
        // 2. TẠO CÔNG TY
        // =========================
        public bool Create(TaoCongTyDto dto, Guid maNguoiDung)
        {
            if (dto == null) return false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO CongTy
                    (MaCongTy, TenCongTy, Website, MoTa, TaoBoi, NgayTao, NgayCapNhat)
                    VALUES
                    (@MaCongTy, @TenCongTy, @Website, @MoTa, @TaoBoi, GETDATE(), GETDATE())";

                cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier)
                              .Value = Guid.NewGuid();

                cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 255)
                              .Value = dto.TenCongTy;

                cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 255)
                              .Value = (object?)dto.Website ?? DBNull.Value;

                cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar)
                              .Value = (object?)dto.MoTa ?? DBNull.Value;

                cmd.Parameters.Add("@TaoBoi", SqlDbType.UniqueIdentifier)
                              .Value = maNguoiDung;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // =========================
        // 3. LẤY CHI TIẾT CÔNG TY
        // =========================
        public CongTyDto? GetById(Guid maCongTy, Guid maNguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT MaCongTy, TenCongTy, Website, MoTa
                    FROM CongTy
                    WHERE MaCongTy = @MaCongTy
                      AND TaoBoi = @MaNguoiDung";

                cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier)
                              .Value = maCongTy;

                cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier)
                              .Value = maNguoiDung;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CongTyDto
                        {
                            MaCongTy = reader.GetGuid(0),
                            TenCongTy = reader.GetString(1),
                            Website = reader.IsDBNull(2) ? null : reader.GetString(2),
                            MoTa = reader.IsDBNull(3) ? null : reader.GetString(3)
                        };
                    }
                }
            }

            return null;
        }

        // =========================
        // 4. CẬP NHẬT CÔNG TY
        // =========================
        public bool Update(Guid maCongTy, TaoCongTyDto dto, Guid maNguoiDung)
        {
            if (dto == null) return false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    UPDATE CongTy
                    SET TenCongTy = @TenCongTy,
                        Website = @Website,
                        MoTa = @MoTa,
                        NgayCapNhat = GETDATE()
                    WHERE MaCongTy = @MaCongTy
                      AND TaoBoi = @MaNguoiDung";

                cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier)
                              .Value = maCongTy;

                cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 255)
                              .Value = dto.TenCongTy;

                cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 255)
                              .Value = (object?)dto.Website ?? DBNull.Value;

                cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar)
                              .Value = (object?)dto.MoTa ?? DBNull.Value;

                cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier)
                              .Value = maNguoiDung;

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
