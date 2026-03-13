using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Domein.Interfaces;

public interface IPersoonRepositoryBestandRead
{
    // R operaties
    PersoonDto GetById(string id);
    IEnumerable<PersoonDto> GetAll();
}

public interface IPersoonRepositoryDatabaseRead
{
    // R operaties
    /// <summary>
    /// Haalt de persoon op basis van het id (RijksregisterNummer) uit de database.
    /// Er is een mogelijkheid dat er meerdere personen zijn met hetzelfde RijksregisterNummer, daarom wordt er een lijst van PersoonDto's geretourneerd.
    /// </summary>
    /// <param name="id">Het RijksregisterNummer van de persoon</param>
    /// <returns>Een lijst van PersoonDto's die overeenkomen met het opgegeven id</returns>
    IEnumerable<PersoonDto> GetById(string id);
    IEnumerable<PersoonDto> GetAll();
}

public interface IPersoonRepositoryCRUD : IPersoonRepositoryDatabaseRead
{
    void Insert(PersoonDto product);
    void Update(PersoonDto product);
    void Delete(string id);
}
