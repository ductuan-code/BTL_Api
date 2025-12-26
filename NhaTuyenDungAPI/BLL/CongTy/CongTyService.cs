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

        public bool TaoCongTy(TaoCongTyDto dto, Guid maNguoiDung)
            => _repo.Create(dto, maNguoiDung);

        public bool CapNhat(Guid maCongTy, CapNhatCongTyDto dto)
            => _repo.Update(maCongTy, dto);

        public bool Xoa(Guid maCongTy)
            => _repo.Delete(maCongTy);
    }
}
