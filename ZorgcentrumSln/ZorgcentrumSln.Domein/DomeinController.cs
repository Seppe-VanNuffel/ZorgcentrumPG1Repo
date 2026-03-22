using ZorgcentrumSln.Domein.Dtos;
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
}