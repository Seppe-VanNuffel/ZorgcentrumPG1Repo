using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Factories;
using ZorgcentrumSln.Domein.Interfaces;
using ZorgcentrumSln.Domein.Models.Afspraak;
using ZorgcentrumSln.Domein.Models.Persoon;

namespace ZorgcentrumSln.Domein;

public class DomeinController(IPersoonRepositoryBestandRead pRR, IAfspraakRepositoryRead aRR)
{
    private IPersoonRepositoryBestandRead _pRR = pRR;
    private IAfspraakRepositoryRead _aRR = aRR;

    public List<PersoonDto> GetAllPersonen()
    {
        IEnumerable<PersoonDto> persoonInfo = _pRR.GetAll();
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
        IEnumerable<AfspraakDto> AfspraakInfo = _aRR.GetAll();
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
}
