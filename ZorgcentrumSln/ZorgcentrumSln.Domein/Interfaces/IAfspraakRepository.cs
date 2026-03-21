using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Domein.Interfaces;

public interface IAfspraakRepositoryRead
{
    // R operaties
    AfspraakDto GetById(string id);
    IEnumerable<AfspraakDto> GetAll();
}

public interface IAfspraakRepositoryCRUD : IAfspraakRepositoryRead
{
    void Insert(AfspraakDto product);
    void InsertOld(AfspraakDto product);
    void Update(AfspraakDto product);
    void Delete(string id);
    public void ClearAllAfspraken();
}