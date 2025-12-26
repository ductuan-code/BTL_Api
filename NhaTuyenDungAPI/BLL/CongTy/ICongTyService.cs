using DTO.CongTy;

namespace BLL.CongTy
{
    public interface ICongTyService
    {
        List<CongTyDto> GetMyCompanies(Guid maNguoiDung);
        bool TaoCongTy(TaoCongTyDto dto, Guid maNguoiDung);
        bool CapNhat(Guid maCongTy, CapNhatCongTyDto dto);
        bool Xoa(Guid maCongTy);
    }
}
