using DAL.DonUngTuyen;
using DTO.DonUngTuyen;

namespace BLL.DonUngTuyen
{
    public class DonUngTuyenService : IDonUngTuyenService
    {
        private readonly IDonUngTuyenRepository _repo;

        public DonUngTuyenService(IDonUngTuyenRepository repo)
        {
            _repo = repo;
        }

        public bool TaoDon(Guid maUngVien, TaoDonUngTuyenDto dto)
            => _repo.TaoDon(maUngVien, dto);

        public List<DonUngTuyenDto> LayTheoViecLam(Guid maViecLam)
            => _repo.LayTheoViecLam(maViecLam);

        public DonUngTuyenDto? LayChiTiet(Guid maDon)
            => _repo.LayChiTiet(maDon);

        public bool CapNhatTrangThai(Guid maDon, string trangThai)
            => _repo.CapNhatTrangThai(maDon, trangThai);
    }
}
