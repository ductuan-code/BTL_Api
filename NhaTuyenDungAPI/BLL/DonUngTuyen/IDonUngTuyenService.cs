using DTO.DonUngTuyen;

namespace BLL.DonUngTuyen
{
    public interface IDonUngTuyenService
    {
        bool TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto);
        List<DonUngTuyenDto> LayTheoUngVien(Guid maUngVien);
        List<DonUngTuyenDto> LayTheoViecLam(Guid maViecLam);
        bool CapNhatTrangThai(Guid maDon, string trangThai);
    }
}
