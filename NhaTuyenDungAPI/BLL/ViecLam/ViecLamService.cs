using DAL.ViecLam;
using DTO.ViecLam;
using NhaTuyenDungAPI.DTO.ViecLam;

namespace BLL.ViecLam
{
    public class ViecLamService : IViecLamService
    {
        private readonly IViecLamRepository _repo;

        public ViecLamService(IViecLamRepository repo)
        {
            _repo = repo;
        }

        public bool TaoViecLam(TaoViecLamDto dto, Guid maNguoiDung)
            => _repo.Create(dto, maNguoiDung);

        public List<ViecLamDto> LayDanhSach(Guid maNguoiDung)
            => _repo.GetByEmployer(maNguoiDung);

        public ViecLamDto? ChiTiet(Guid maViecLam)
            => _repo.GetDetail(maViecLam);

        public bool CapNhat(Guid maViecLam, CapNhatViecLamDto dto)
            => _repo.Update(maViecLam, dto);

        public bool AnHien(Guid maViecLam)
            => _repo.Toggle(maViecLam);

        public bool Xoa(Guid maViecLam)
            => _repo.Delete(maViecLam);
    }
}
