using DTO.DonUngTuyen;

namespace BLL.DonUngTuyen
{
    public interface IDonUngTuyenService
    {
        bool TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto);
        List<DonUngTuyenDto> LayTheoViecLam(Guid maViecLam);
        DonUngTuyenDto? LayChiTiet(Guid maDon);
        bool CapNhatTrangThai(Guid maDon, string trangThai);
    }
}
