using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Factories;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.Domein.Models.Afspraak;
using ZorgcentrumSln.Domein.Models.Persoon;

namespace ZorgcentrumSln.Domein;

public class DomeinController(IPersoonRepositoryRead pRR, IAfspraakRepositoryRead aRR)
{
    private IPersoonRepositoryRead _pRR = pRR;
    private IAfspraakRepositoryRead _aRR = aRR;

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

    public List<PersoonDto> GetAllPatienten()
    {
        return GetAllPersonen().Where(x => x.GetType().Name == "PatientPersoonDto").ToList();
    }
    public List<PersoonDto> GetAllArtsen()
    {
        return GetAllPersonen().Where(x => x.GetType().Name == "ArtsPersoonDto").ToList();
    }

    public List<AfspraakDto> GetAllAfspraken()
    {
        IEnumerable<AfspraakDto> AfspraakInfo = _aRR.GetAll();
        List<Afspraak> Afspraken = new();

        foreach (var item in AfspraakInfo)
        {
            Afspraken.Add(AfspraakRelatedFactories.CreateAfspraakFromAfspraakDto(item));
        }

        List<AfspraakDto> AfspraakDtos = [];

        foreach (var item in Afspraken)
        {
            AfspraakDtos.Add(AfspraakRelatedFactories.CreateAfspraakDtoFromAfspraak(item));
        }

        return AfspraakDtos;
    }
}
