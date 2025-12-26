using DTO.CongTy;

namespace DAL.CongTy
{
    public interface ICongTyRepository
    {
        List<CongTyDto> GetByEmployer(Guid maNguoiDung);
        CongTyDto? GetById(Guid maCongTy, Guid maNguoiDung);
        bool Create(TaoCongTyDto dto, Guid maNguoiDung);
        bool Update(Guid maCongTy, TaoCongTyDto dto, Guid maNguoiDung);
    }
}
