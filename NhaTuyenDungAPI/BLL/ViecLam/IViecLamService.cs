using DTO.ViecLam;
using NhaTuyenDungAPI.DTO.ViecLam;

namespace BLL.ViecLam
{
    public interface IViecLamService
    {
        bool TaoViecLam(TaoViecLamDto dto, Guid maNguoiDung);
        List<ViecLamDto> LayDanhSach(Guid maNguoiDung);
        ViecLamDto? ChiTiet(Guid maViecLam);
        bool CapNhat(Guid maViecLam, CapNhatViecLamDto dto);
        bool AnHien(Guid maViecLam);
        bool Xoa(Guid maViecLam);
    }
}
