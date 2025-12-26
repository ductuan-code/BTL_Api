using DAL.CongTy;
using DTO.CongTy;
using System.Text.RegularExpressions;

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

        public CongTyDto? GetById(Guid maCongTy)
            => _repo.GetById(maCongTy);

        public bool TaoCongTy(TaoCongTyDto dto, Guid maNguoiDung)
        {
            string slug = GenerateSlug(dto.TenCongTy);
            return _repo.Create(dto, slug, maNguoiDung);
        }

        public bool CapNhat(Guid maCongTy, CapNhatCongTyDto dto)
            => _repo.Update(maCongTy, dto);

        public bool Xoa(Guid maCongTy)
            => _repo.Delete(maCongTy);

        private string GenerateSlug(string text)
        {
            text = text.ToLower().Trim();
            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            text = Regex.Replace(text, @"\s+", "-");
            return text + "-" + Guid.NewGuid().ToString("N")[..6];
        }
    }
}
