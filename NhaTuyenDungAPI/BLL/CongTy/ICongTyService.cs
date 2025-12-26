using DTO.CongTy;

namespace BLL.CongTy
{
    public interface ICongTyService
    {
        List<CongTyDto> GetMyCompanies(Guid maNguoiDung);
        CongTyDto? GetDetail(Guid maCongTy, Guid maNguoiDung);
        bool TaoCongTy(TaoCongTyDto dto, Guid maNguoiDung);
        bool CapNhatCongTy(Guid maCongTy, TaoCongTyDto dto, Guid maNguoiDung);
    }
}
