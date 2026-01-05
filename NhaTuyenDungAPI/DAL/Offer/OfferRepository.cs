using DTO.Offer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Offer
{
    public class OfferRepository : IOfferRepository
    {
        private readonly string _conn;

        public OfferRepository(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection");
        }

        public bool TaoOffer(TaoOfferDto dto, Guid maNguoiGui)
        {
            using var conn = new SqlConnection(_conn);
            var cmd = new SqlCommand(@"
                INSERT INTO ThuMoiLamViec
                (MaOffer, MaDon, MaNguoiGui, MucLuongDeXuat, DonViTienTe, PhucLoi, TrangThai, NgayGui)
                VALUES
                (@MaOffer, @MaDon, @MaNguoiGui, @Luong, @TienTe, @PhucLoi, 'pending', GETDATE())
            ", conn);

            cmd.Parameters.AddWithValue("@MaOffer", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@MaDon", dto.MaDon);
            cmd.Parameters.AddWithValue("@MaNguoiGui", maNguoiGui);
            cmd.Parameters.AddWithValue("@Luong", dto.MucLuongDeXuat);
            cmd.Parameters.AddWithValue("@TienTe", dto.DonViTienTe);
            cmd.Parameters.AddWithValue("@PhucLoi", (object?)dto.PhucLoi ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public OfferDto GetByDon(Guid maDon)
        {
            using var conn = new SqlConnection(_conn);
            var cmd = new SqlCommand(@"
                SELECT MaOffer, MaDon, MucLuongDeXuat, DonViTienTe, PhucLoi, TrangThai, NgayGui
                FROM ThuMoiLamViec
                WHERE MaDon = @MaDon
            ", conn);

            cmd.Parameters.AddWithValue("@MaDon", maDon);
            conn.Open();

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new OfferDto
            {
                MaOffer = rd.GetGuid(0),
                MaDon = rd.GetGuid(1),
                MucLuongDeXuat = rd.GetInt32(2),
                DonViTienTe = rd.GetString(3),
                PhucLoi = rd.IsDBNull(4) ? null : rd.GetString(4),
                TrangThai = rd.GetString(5),
                NgayGui = rd.GetDateTime(6)
            };
        }

        public bool CapNhatTrangThai(Guid maOffer, string trangThai)
        {
            using var conn = new SqlConnection(_conn);
            var cmd = new SqlCommand(@"
                UPDATE ThuMoiLamViec
                SET TrangThai = @TrangThai, NgayPhanHoi = GETDATE()
                WHERE MaOffer = @MaOffer
            ", conn);

            cmd.Parameters.AddWithValue("@MaOffer", maOffer);
            cmd.Parameters.AddWithValue("@TrangThai", trangThai);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
