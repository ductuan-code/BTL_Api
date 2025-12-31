using DTO.ViecLam;
using NhaTuyenDungAPI.DTO.ViecLam;

namespace DAL.ViecLam
{
    public interface IViecLamRepository
    {
        bool Create(TaoViecLamDto dto, Guid maNguoiDung);
        List<ViecLamDto> GetByEmployer(Guid maNguoiDung);
        ViecLamDto? GetDetail(Guid maViecLam);
        bool Update(Guid maViecLam, CapNhatViecLamDto dto);
        bool Toggle(Guid maViecLam);
        bool Delete(Guid maViecLam);
    }
}
