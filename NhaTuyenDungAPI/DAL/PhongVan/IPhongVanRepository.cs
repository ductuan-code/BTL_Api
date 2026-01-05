using DTO.PhongVan;

namespace DAL.PhongVan
{
    public interface IPhongVanRepository
    {
        bool Tao(Guid maNguoiDung, TaoPhongVanDto dto);
        List<PhongVanDto> LayTheoDon(Guid maDon);
        PhongVanDto? LayChiTiet(Guid maPhongVan);
        bool CapNhat(Guid maPhongVan, CapNhatPhongVanDto dto);
        bool CapNhatTrangThai(Guid maPhongVan, string trangThai);
        List<PhongVanDto> LayTheoNhaTuyenDung(Guid maNguoiDung);
    }
}
