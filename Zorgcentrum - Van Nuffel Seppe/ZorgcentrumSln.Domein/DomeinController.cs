using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Factories;
using ZorgcentrumSln.Domein.Interfaces;

namespace ZorgcentrumSln.Domein;

public class DomeinController
{
    private readonly IAfspraakRepositoryCRUD _afspraakRepository;
    private readonly IPersoonRepositoryCRUD _persoonRepository;

    public DomeinController(IAfspraakRepositoryCRUD afspraakRepository, IPersoonRepositoryCRUD persoonRepository)
    {
        _afspraakRepository = afspraakRepository;
        _persoonRepository = persoonRepository;
    }

    public IEnumerable<AfspraakDto> GetAllAfspraken()
    {
        return _afspraakRepository.GetAll().ToHashSet();
    }

    public IEnumerable<PersoonDto> GetAllPersonen()
    {
        return _persoonRepository.GetAll();
    }

    public IEnumerable<PersoonDto> GetAllArtsen()
    {
        return GetAllPersonen().OfType<ArtsPersoonDto>();
    }

    public IEnumerable<PersoonDto> GetAllPatienten()
    {
        return GetAllPersonen().OfType<PatientPersoonDto>();
    }

    public IEnumerable<AfspraakDto> GetAfsprakenByFilter(string filter)
    {
        return _afspraakRepository.GetAllByFilter(filter);
    }

    public IEnumerable<string> GetAllAfdelingCodes()
    {
        return _afspraakRepository.GetAllAfdelingen();
    }

    public void DeleteAfspraak(int id)
    {
        _afspraakRepository.Delete(id.ToString());
    }

    public void InsertAfspraak(AfspraakDto afspraakDto)
    {
        var afspraak = AfspraakRelatedFactories.CreateAfspraakFromAfspraakDto(afspraakDto);

        _afspraakRepository.Insert(AfspraakRelatedFactories.CreateAfspraakDtoFromAfspraak(afspraak));
    }

    public void UpdateAfspraak(AfspraakDto afspraakDto)
    {
        var afspraak = AfspraakRelatedFactories.CreateAfspraakFromAfspraakDto(afspraakDto);

        _afspraakRepository.Update(AfspraakRelatedFactories.CreateAfspraakDtoFromAfspraak(afspraak));
    }

    public void InsertPersoon(PersoonDto persoonDto)
    {
        var persoon = PersoonRelatedFactories.CreatePersoonFromPersoonDto(persoonDto);

        _persoonRepository.Insert(PersoonRelatedFactories.CreatePersoonDtoFromPersoon(persoon));
    }

    public void UpdatePersoon(PersoonDto persoonDto)
    {
        var persoon = PersoonRelatedFactories.CreatePersoonFromPersoonDto(persoonDto);

        _persoonRepository.Update(PersoonRelatedFactories.CreatePersoonDtoFromPersoon(persoon));
    }

    public IEnumerable<PersoonDto> GetAllPersonenByFilter(string filter)
    {
        return _persoonRepository.GetAllByFilter(filter);
    }
    public IEnumerable<PersoonDto> GetAllPatientenByFilter(string filter)
    {
        return _persoonRepository.GetPatientenByFilter(filter);
    }
}