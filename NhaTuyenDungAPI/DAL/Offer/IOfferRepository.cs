using DTO.Offer;

namespace DAL.Offer
{
    public interface IOfferRepository
    {
        bool TaoOffer(TaoOfferDto dto, Guid maNguoiGui);
        OfferDto GetByDon(Guid maDon);
        bool CapNhatTrangThai(Guid maOffer, string trangThai);
    }
}
