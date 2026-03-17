using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.PersistentieBestand.BestandsBeheer;

namespace ZorgcentrumSln.PersistentieBestand;

public class PersoonRepositoryBestand : IPersoonRepositoryBestandRead
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

    public PersoonDto GetById(string id)
    {
        throw new NotImplementedException();
    }
}
