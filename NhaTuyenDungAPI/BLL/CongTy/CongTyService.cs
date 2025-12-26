using DAL.CongTy;
using DTO.CongTy;

namespace BLL.CongTy
{
    public class CongTyService : ICongTyService
    {
        private readonly ICongTyRepository _repo;

        public CongTyService(ICongTyRepository repo)
        {
            _repo = repo;
        }

        public List<CongTyDto> GetMyCompanies(Guid maNguoiDung)
            => _repo.GetByEmployer(maNguoiDung);

        public CongTyDto? GetDetail(Guid maCongTy, Guid maNguoiDung)
            => _repo.GetById(maCongTy, maNguoiDung);

        public bool TaoCongTy(TaoCongTyDto dto, Guid maNguoiDung)
            => _repo.Create(dto, maNguoiDung);

        public bool CapNhatCongTy(Guid maCongTy, TaoCongTyDto dto, Guid maNguoiDung)
            => _repo.Update(maCongTy, dto, maNguoiDung);
    }
}
