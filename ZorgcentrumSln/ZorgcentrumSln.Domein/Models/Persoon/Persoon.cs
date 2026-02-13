namespace ZorgcentrumSln.Domein.Models.Persoon;

internal abstract class Persoon(string rijksregisterNummer, string naam, string voornaam)
{
    public string RijksregisterNummer { get; } = rijksregisterNummer;

    public string Naam { get; } = naam;

    public string Voornaam { get; } = voornaam;

}
