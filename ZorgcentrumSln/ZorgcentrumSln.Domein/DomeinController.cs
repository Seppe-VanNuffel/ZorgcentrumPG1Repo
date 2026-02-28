
using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Factories;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.Domein.Models.Persoon;

namespace ZorgcentrumSln.Domein;

public class DomeinController(IPersoonRepositoryRead pRR)
{
    private IPersoonRepositoryRead _pRR = pRR;

    public List<PersoonDto> GetAllPersonen()
    {
        IEnumerable<PersoonDto> persoonInfo = _pRR.GetAll();
        List<Persoon> personen = new();

        foreach (var item in persoonInfo)
        {
            personen.Add(PersoonRelatedFactories.CreatePersoonFromPersoonDto(item));
        }

        List<PersoonDto> persoonDtos = [];

        foreach (var item in personen)
        {
            persoonDtos.Add(PersoonRelatedFactories.CreatePersoonDtoFromPersoon(item));
        }

        return persoonDtos;
    }
}
