public class DonUngTuyenBLL
{
    private readonly DonUngTuyenDAL _dal;

    public DonUngTuyenBLL(DonUngTuyenDAL dal)
    {
        _dal = dal;
    }

    public List<object> GetAll() => _dal.GetAll();

    public void Create(CreateDonUngTuyenDto dto)
    {
        _dal.Create(dto);
    }

    public void UpdateTrangThai(Guid id, string trangThai)
    {
        _dal.UpdateTrangThai(id, trangThai);
    }
}

