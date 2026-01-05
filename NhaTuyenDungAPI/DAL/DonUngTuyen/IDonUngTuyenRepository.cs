using DTO.DonUngTuyen;

namespace DAL.DonUngTuyen
{
    public interface IDonUngTuyenRepository
    {
        bool TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto);
        List<DonUngTuyenDto> LayTheoUngVien(Guid maUngVien);
        List<DonUngTuyenDto> LayTheoViecLam(Guid maViecLam);
        bool CapNhatTrangThai(Guid maDon, string trangThai);
    }
}
