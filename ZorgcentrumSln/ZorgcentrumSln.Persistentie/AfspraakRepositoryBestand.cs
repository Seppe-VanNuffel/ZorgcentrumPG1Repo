using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.PersistentieBestand.BestandsBeheer;

namespace ZorgcentrumSln.PersistentieBestand;

public class AfspraakRepositoryBestand : IAfspraakRepositoryRead
{
    private List<AfspraakDto> afspraakDtos;

    public AfspraakRepositoryBestand(string padNaarBestand)
    {
        afspraakDtos = BestandsVerwerking.LeesAfspraakBestand(padNaarBestand);
    }

    public IEnumerable<AfspraakDto> GetAll()
    {
        return afspraakDtos;
    }

    public AfspraakDto GetById(string id)
    {
        throw new NotImplementedException();
    }
}
