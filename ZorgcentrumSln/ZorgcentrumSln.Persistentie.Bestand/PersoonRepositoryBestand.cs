using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.PersistentieBestand.BestandsBeheer;

namespace ZorgcentrumSln.PersistentieBestand;

public class PersoonRepositoryBestand : IPersoonRepositoryRead
{
    private List<PersoonDto> _PersonenInfo;

    public PersoonRepositoryBestand(string padNaarArtsBestand, string padNaarPatientBestand)
    {
        _PersonenInfo = BestandsVerwerking.LeesPersoonBestanden(padNaarArtsBestand, padNaarPatientBestand);
    }

    public IEnumerable<PersoonDto> GetAll()
    {
        return _PersonenInfo;
    }

    public PersoonDto GetArtsById(string id)
    {
        throw new NotImplementedException();
    }

    public PersoonDto GetPatientById(string id)
    {
        throw new NotImplementedException();
    }
}
