namespace DTO.PhongVan
{
    public class PhongVanDto
    {
        public Guid MaPhongVan { get; set; }
        public Guid MaDon { get; set; }
        public DateTime ThoiGian { get; set; }
        public int? ThoiLuong { get; set; }
        public string? DiaDiem { get; set; }
        public string TrangThai { get; set; } = "";
        public string? GhiChu { get; set; }
    }
}
