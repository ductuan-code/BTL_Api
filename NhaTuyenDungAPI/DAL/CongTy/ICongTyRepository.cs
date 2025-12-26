using DTO.CongTy;

namespace DAL.CongTy
{
    public interface ICongTyRepository
    {
        List<CongTyDto> GetByEmployer(Guid maNguoiDung);
        CongTyDto? GetById(Guid maCongTy);
        bool Create(TaoCongTyDto dto, string slug, Guid maNguoiDung);
        bool Update(Guid maCongTy, CapNhatCongTyDto dto);
        bool Delete(Guid maCongTy);
    }
}
