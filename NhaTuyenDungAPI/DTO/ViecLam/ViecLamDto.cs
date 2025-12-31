namespace DTO.ViecLam
{
    public class ViecLamDto
    {
        public Guid MaViecLam { get; set; }
        public string TieuDe { get; set; }
        public string Slug { get; set; }
        public string DiaDiem { get; set; }
        public string LoaiHinhCongViec { get; set; }
        public int? LuongToiThieu { get; set; }
        public int? LuongToiDa { get; set; }
        public bool DaDuyet { get; set; }
        public bool DuocHienThi { get; set; }
        public DateTime NgayDang { get; set; }
    }
}
