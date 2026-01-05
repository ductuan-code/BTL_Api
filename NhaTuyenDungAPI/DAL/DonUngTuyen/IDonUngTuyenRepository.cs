using DTO.DonUngTuyen;

namespace DAL.DonUngTuyen
{
    public interface IDonUngTuyenRepository
    {
        bool TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto);
        List<DonUngTuyenDto> LayTheoViecLam(Guid maViecLam);
        DonUngTuyenDto? LayChiTiet(Guid maDon);
        bool CapNhatTrangThai(Guid maDon, string trangThai);
    }
}
