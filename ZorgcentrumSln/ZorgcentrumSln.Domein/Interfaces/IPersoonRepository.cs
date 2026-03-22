using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Domein.Interfaces;

public interface IPersoonRepositoryRead
{
    // R operaties
    PersoonDto GetPatientById(string id);
    PersoonDto GetArtsById(string id);
    IEnumerable<PersoonDto> GetAll();
}

public interface IPersoonRepositoryCRUD : IPersoonRepositoryRead
{
    void Insert(PersoonDto persoon);
    void Update(PersoonDto persoon);
    void DeletePatient(string id);
    void DeleteArts(string id);
    void Delete(string id);

    public void ClearAllPersonen();
    int GetAmount();
}
