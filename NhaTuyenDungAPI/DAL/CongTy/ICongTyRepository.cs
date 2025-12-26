using DTO.CongTy;

namespace DAL.CongTy
{
    public interface ICongTyRepository
    {
        List<CongTyDto> GetByEmployer(Guid maNguoiDung);
        bool Create(TaoCongTyDto dto, Guid maNguoiDung);
        bool Update(Guid maCongTy, CapNhatCongTyDto dto);
        bool Delete(Guid maCongTy);
    }
}
