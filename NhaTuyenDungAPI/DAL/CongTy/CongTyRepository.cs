using DTO.CongTy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL.CongTy
{
    public class CongTyRepository : ICongTyRepository
    {
        private readonly string _connectionString;

        public CongTyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ====== TẠO SLUG ======
        private string TaoSlug(string input)
        {
            input = input.ToLowerInvariant();
            input = Regex.Replace(input, @"[^a-z0-9\s-]", "");
            input = Regex.Replace(input, @"\s+", "-").Trim('-');
            return input;
        }

        // ====== GET ======
        public List<CongTyDto> GetByEmployer(Guid maNguoiDung)
        {
            var list = new List<CongTyDto>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
        SELECT MaCongTy, TenCongTy, Slug, Website, MoTa
        FROM CongTy
        WHERE TaoBoi = @MaNguoiDung";

            cmd.Parameters.Add("@MaNguoiDung", SqlDbType.UniqueIdentifier)
                          .Value = maNguoiDung;

            conn.Open();
            using SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                list.Add(new CongTyDto
                {
                    MaCongTy = rd.GetGuid(0),
                    TenCongTy = rd.GetString(1),

                    // ✅ FIX NULL
                    Slug = rd.IsDBNull(2) ? "" : rd.GetString(2),
                    Website = rd.IsDBNull(3) ? null : rd.GetString(3),
                    MoTa = rd.IsDBNull(4) ? null : rd.GetString(4)
                });
            }

            return list;
        }


        // ====== CREATE ======
        public bool Create(TaoCongTyDto dto, Guid maNguoiDung)
        {
            string slug = TaoSlug(dto.TenCongTy);

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO CongTy
                (MaCongTy, TenCongTy, Slug, Website, MoTa, TaoBoi, NgayTao, NgayCapNhat)
                VALUES
                (@MaCongTy, @TenCongTy, @Slug, @Website, @MoTa, @TaoBoi, GETDATE(), GETDATE())";

            cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 255).Value = dto.TenCongTy;
            cmd.Parameters.Add("@Slug", SqlDbType.NVarChar, 255).Value = slug;
            cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 255).Value = (object?)dto.Website ?? DBNull.Value;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)dto.MoTa ?? DBNull.Value;
            cmd.Parameters.Add("@TaoBoi", SqlDbType.UniqueIdentifier).Value = maNguoiDung;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ====== UPDATE ======
        public bool Update(Guid maCongTy, CapNhatCongTyDto dto)
        {
            string slug = TaoSlug(dto.TenCongTy);

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE CongTy
                SET TenCongTy = @TenCongTy,
                    Slug = @Slug,
                    Website = @Website,
                    MoTa = @MoTa,
                    NgayCapNhat = GETDATE()
                WHERE MaCongTy = @MaCongTy";

            cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier).Value = maCongTy;
            cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 255).Value = dto.TenCongTy;
            cmd.Parameters.Add("@Slug", SqlDbType.NVarChar, 255).Value = slug;
            cmd.Parameters.Add("@Website", SqlDbType.NVarChar, 255).Value = (object?)dto.Website ?? DBNull.Value;
            cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)dto.MoTa ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ====== DELETE ======
        public bool Delete(Guid maCongTy)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM CongTy WHERE MaCongTy = @MaCongTy";
            cmd.Parameters.Add("@MaCongTy", SqlDbType.UniqueIdentifier).Value = maCongTy;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
