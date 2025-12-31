namespace NhaTuyenDungAPI.DTO.ViecLam
{
    public class CapNhatViecLamDto
    {
        public string TieuDe { get; set; }
        public string DiaDiem { get; set; }
        public string LoaiHinhCongViec { get; set; }
        public string MoTa { get; set; }
        public string YeuCau { get; set; }
        public string TrachNhiem { get; set; }
        public int? LuongToiThieu { get; set; }
        public int? LuongToiDa { get; set; }
        public DateTime? NgayHetHan { get; set; }
    }
}
