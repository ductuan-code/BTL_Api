public class NguoiDungBLL
{
    private readonly NguoiDungDAL _dal;

    public NguoiDungBLL(NguoiDungDAL dal)
    {
        _dal = dal;
    }

    public List<object> GetAllNguoiDung()
    {
        return _dal.GetAll();
    }

    public void CreateNguoiDung(CreateNguoiDungDto dto)
    {
        // Có thể thêm validate nếu muốn
        _dal.Create(dto);
    }
    public void UpdateNguoiDung(Guid id, UpdateNguoiDungDto dto)
    {
        _dal.Update(id, dto);
    }
    public void DeleteNguoiDung(Guid id)
    {
        _dal.Delete(id);
    }


}

