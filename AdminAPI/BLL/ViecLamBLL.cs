using Microsoft.AspNetCore.Mvc;

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
    public void UpdateViecLam(Guid id, UpdateViecLamDto dto)
    {
        _dal.Update(id, dto);
    }
    public void DuyetViecLam(Guid maViecLam)
    {
        _dal.DuyetViecLam(maViecLam);
    }
    public void AnHienViecLam(Guid maViecLam, bool hienThi)
    {
        _dal.SetTrangThaiHienThi(maViecLam, hienThi);
    }
    public List<object> GetViecLamChuaDuyet()
    {
        return _dal.GetViecLamChuaDuyet();
    }
    public List<object> Search(string keyword)
    {
        return _dal.Search(keyword);
    }
    public List<object> FilterByDiaDiem(string diaDiem)
    {
        return _dal.FilterByDiaDiem(diaDiem);
    }
    public List<object> FilterByLuong(int min, int max)
    {
        return _dal.FilterByLuong(min, max);
    }


}

