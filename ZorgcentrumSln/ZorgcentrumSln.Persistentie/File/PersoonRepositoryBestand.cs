using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.Persistentie.File.BestandsBeheer;

namespace ZorgcentrumSln.Persistentie.File;

public class PersoonRepositoryBestand : IPersoonRepositoryRead
{
    private List<PersoonDto> _PersonenInfo;

    public PersoonRepositoryBestand(string padNaarArtsBestand, string padNaarPatientBestand)
    {
        _PersonenInfo = BestandsVerwerking.LeesBestanden(padNaarArtsBestand, padNaarPatientBestand);
    }

    public IEnumerable<PersoonDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public PersoonDto GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
