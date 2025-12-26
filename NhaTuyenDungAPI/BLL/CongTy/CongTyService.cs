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
        {
            return _repo.GetByEmployer(maNguoiDung);
        }

        public bool TaoCongTy(TaoCongTyDto dto, Guid maNguoiDung)
        {
            return _repo.Create(dto, maNguoiDung);
        }
    }
}
