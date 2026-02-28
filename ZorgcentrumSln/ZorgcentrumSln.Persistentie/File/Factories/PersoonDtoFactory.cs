using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Persistentie.File.Factories;

internal class PersoonDtoFactory
{
    internal static PersoonDto MapToPersoonDto(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new ArgumentException("Ingelezen lijn is null, niet ingevuld of leeg", nameof(line));

        var data = line.Split(';');

        return data.Length switch
        {
            4 => MapToPatientDto(data),
            6 => MapToArtsDto(data),
            _ => throw new ArgumentOutOfRangeException(nameof(line), "Ingelezen lijn heeft niet genoeg data")
        };
    }

    private static PersoonDto MapToArtsDto(string[] data)
    {
        var persoon = MapToPersoonTuple(data);
        string specialisatie = data[4];
        HashSet<string> afdelingen = data[5].Split('|').ToHashSet();

        return new ArtsPersoonDto(persoon.rijksregisterNummer, persoon.naam, persoon.voornaam, data[3], specialisatie, afdelingen);
    }
    private static PersoonDto MapToPatientDto(string[] data)
    {
        var persoon = MapToPersoonTuple(data);

        HashSet<string> afdelingen = data[3].Split('|').ToHashSet();

        return new PatientPersoonDto(persoon.rijksregisterNummer, persoon.naam, persoon.voornaam, afdelingen);
    }

    private static (string rijksregisterNummer, string naam, string voornaam) MapToPersoonTuple(string[] data)
    {
        (string, string, string) Persoon = (data[0], data[1], data[2]);
        return Persoon;
    }
}
