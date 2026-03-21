using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Factories;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.Domein.Models.Afspraak;
using ZorgcentrumSln.Domein.Models.Persoon;

namespace ZorgcentrumSln.Domein;

public class GegevensInitDomeinController(IPersoonRepositoryRead pRRb, IAfspraakRepositoryRead aRRb, IPersoonRepositoryCRUD pRCRUD, IAfspraakRepositoryCRUD aRCRUD)
{
    private IPersoonRepositoryRead _pRRb = pRRb;
    private IAfspraakRepositoryRead _aRRb = aRRb;
    private IPersoonRepositoryCRUD _pRCRUD = pRCRUD;
    private IAfspraakRepositoryCRUD _aRCRUD = aRCRUD;

    public List<PersoonDto> GetAllPersonen()
    {
        IEnumerable<PersoonDto> persoonInfo = _pRRb.GetAll();
        List<Persoon> personen = new();

        foreach (var item in persoonInfo)
        {
            try
            {
                personen.Add(PersoonRelatedFactories.CreatePersoonFromPersoonDto(item));
            }
            catch (Exception e)
            {
                if (e.InnerException?.InnerException != null)
                {
                    Console.WriteLine($"FullError: {e.Message}, {e.InnerException.Message}, {e.InnerException.InnerException.Message}");
                }
                else if(e.InnerException != null && e.InnerException?.InnerException == null)
                {
                    Console.WriteLine($"InnerError: {e.Message}, {e.InnerException.Message}");
                }
                else
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
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
        IEnumerable<AfspraakDto> AfspraakInfo = _aRRb.GetAll();
        List<Afspraak> Afspraken = new();

        foreach (var item in AfspraakInfo)
        {
            try
            {
                Afspraken.Add(AfspraakRelatedFactories.CreateAfspraakFromAfspraakDto(item));
            }
            catch (Exception e)
            {
                if (e.InnerException?.InnerException != null)
                {
                    Console.WriteLine($"FullError: {e.Message}, {e.InnerException.Message}, {e.InnerException.InnerException.Message}");
                }
                else if (e.InnerException != null && e.InnerException?.InnerException == null)
                {
                    Console.WriteLine($"InnerError: {e.Message}, {e.InnerException.Message}");
                }
                else
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }

        List<AfspraakDto> AfspraakDtos = [];

        foreach (var item in Afspraken)
        {
            AfspraakDtos.Add(AfspraakRelatedFactories.CreateAfspraakDtoFromAfspraak(item));
        }

        return AfspraakDtos;
    }

    public void InsertPersoon(PersoonDto persoonDto)
    {
        _pRCRUD.Insert( persoonDto );
    }

    public void InsertAfspraak(AfspraakDto afspraakDto)
    {
        _aRCRUD.InsertOld(afspraakDto);
    }

    public void ClearAllFromDb()
    {
        _aRCRUD.ClearAllAfspraken();
        _pRCRUD.ClearAllPersonen();
    }

    public void ClearAllAfspraken()
    {
        _aRCRUD.ClearAllAfspraken();
    }

    public void ClearAllPersonen()
    {
        _pRCRUD.ClearAllPersonen();
    }
}