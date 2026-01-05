using DTO.Offer;

namespace BLL.Offer
{
    public interface IOfferService
    {
        bool TaoOffer(TaoOfferDto dto, Guid maNguoiGui);
        OfferDto GetByDon(Guid maDon);
        bool CapNhatTrangThai(Guid maOffer, string trangThai);
    }
}
