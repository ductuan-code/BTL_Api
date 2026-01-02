public class CongTyBLL
{
    private readonly CongTyDAL _dal;

    public CongTyBLL(CongTyDAL dal)
    {
        _dal = dal;
    }

    public List<object> GetAll() => _dal.GetAll();

    public void Create(CreateCongTyDto dto)
    {
        if (string.IsNullOrEmpty(dto.TenCongTy))
            throw new Exception("Tên công ty không được trống");

        _dal.Create(dto);
    }

    public void Update(Guid id, UpdateCongTyDto dto)
    {
        _dal.Update(id, dto);
    }

    public void Delete(Guid id)
    {
        _dal.Delete(id);
    }
}

