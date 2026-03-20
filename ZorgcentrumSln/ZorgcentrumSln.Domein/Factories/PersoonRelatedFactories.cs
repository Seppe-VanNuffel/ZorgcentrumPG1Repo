using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Models.Persoon;

namespace ZorgcentrumSln.Domein.Factories;

internal class PersoonRelatedFactories
{
    internal static PersoonDto CreatePersoonDtoFromPersoon(Persoon persoon)
    {
        return persoon switch
        {
            Arts arts => new ArtsPersoonDto(arts.RijksregisterNummer, arts.Naam, arts.Voornaam, arts.RIZIV, arts.Specialisatie, arts.Afdelingen),
            Patient patient => new PatientPersoonDto(patient.RijksregisterNummer, patient.Naam, patient.Voornaam, patient.Afdelingen),
            _ => throw new NotImplementedException()
        };
    }

    internal static Persoon CreatePersoonFromPersoonDto(PersoonDto persoonDto)
    {
        return persoonDto switch
        {
            ArtsPersoonDto APD => new Arts(APD.RijksregisterNummer, APD.Naam, APD.Voornaam, APD.RIZIV, APD.Specialisatie, APD.Afdelingen),
            PatientPersoonDto PPD => new Patient(PPD.RijksregisterNummer, PPD.Naam, PPD.Voornaam, PPD.Afdelingen),
            _ => throw new NotImplementedException()
        };
    }
}
