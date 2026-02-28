using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Domein.Interfaces;

public interface IPersoonRepositoryRead
{
    // R operaties
    PersoonDto GetById(Guid id);
    IEnumerable<PersoonDto> GetAll();
}

public interface IPersoonRepositoryCRUD : IPersoonRepositoryRead
{
    void Insert(PersoonDto product);
    void Update(PersoonDto product);
    void Delete(Guid id);
}
