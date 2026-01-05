using DAL.PhongVan;
using DTO.PhongVan;

namespace BLL.PhongVan
{
    public class PhongVanService : IPhongVanService
    {
        private readonly IPhongVanRepository _repo;

        public PhongVanService(IPhongVanRepository repo)
        {
            _repo = repo;
        }

        public bool Tao(Guid maNguoiDung, TaoPhongVanDto dto)
            => _repo.Tao(maNguoiDung, dto);

        public List<PhongVanDto> LayTheoDon(Guid maDon)
            => _repo.LayTheoDon(maDon);

        public PhongVanDto? LayChiTiet(Guid maPhongVan)
            => _repo.LayChiTiet(maPhongVan);

        public bool CapNhat(Guid maPhongVan, CapNhatPhongVanDto dto)
            => _repo.CapNhat(maPhongVan, dto);

        public bool CapNhatTrangThai(Guid maPhongVan, string trangThai)
            => _repo.CapNhatTrangThai(maPhongVan, trangThai);

        public List<PhongVanDto> LayTheoNhaTuyenDung(Guid maNguoiDung)
            => _repo.LayTheoNhaTuyenDung(maNguoiDung);
    }
}
