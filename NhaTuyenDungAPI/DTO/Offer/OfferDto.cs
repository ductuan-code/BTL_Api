namespace DTO.Offer
{
    public class OfferDto
    {
        public Guid MaOffer { get; set; }
        public Guid MaDon { get; set; }
        public int MucLuongDeXuat { get; set; }
        public string DonViTienTe { get; set; }
        public string PhucLoi { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayGui { get; set; }
    }
}
