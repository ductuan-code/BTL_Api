public class ViecLamBLL
{
    private readonly ViecLamDAL _dal;

    public ViecLamBLL(ViecLamDAL dal)
    {
        _dal = dal;
    }

    public List<object> GetAll() => _dal.GetAll();

    public void Create(CreateViecLamDto dto)
    {
        _dal.Create(dto);
    }
}

