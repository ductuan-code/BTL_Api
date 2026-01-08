public class ThongKeBLL
{
    private readonly ThongKeDAL _dal;

    public ThongKeBLL(ThongKeDAL dal)
    {
        _dal = dal;
    }

    public int TongNguoiDung() => _dal.TongNguoiDung();
    public int TongCongTy() => _dal.TongCongTy();
    public int TongViecLam() => _dal.TongViecLam();
    public int ViecLamChuaDuyet() => _dal.ViecLamChuaDuyet();
    public int TongDonUngTuyen() => _dal.TongDonUngTuyen();
    public object ThongKeTheoNgay() => _dal.ThongKeTheoNgay();
}
