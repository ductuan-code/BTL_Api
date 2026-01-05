namespace DTO.DonUngTuyen
{
    public class DonUngTuyenDto
    {
        public Guid MaDon { get; set; }
        public Guid MaViecLam { get; set; }
        public Guid MaUngVien { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayNop { get; set; }
        public string? GhiChu { get; set; }
    }
}
