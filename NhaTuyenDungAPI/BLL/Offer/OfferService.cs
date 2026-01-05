using DAL.Offer;
using DTO.Offer;

namespace BLL.Offer
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _repo;

        public OfferService(IOfferRepository repo)
        {
            _repo = repo;
        }

        public bool TaoOffer(TaoOfferDto dto, Guid maNguoiGui)
            => _repo.TaoOffer(dto, maNguoiGui);

        public OfferDto GetByDon(Guid maDon)
            => _repo.GetByDon(maDon);

        public bool CapNhatTrangThai(Guid maOffer, string trangThai)
            => _repo.CapNhatTrangThai(maOffer, trangThai);
    }
}
