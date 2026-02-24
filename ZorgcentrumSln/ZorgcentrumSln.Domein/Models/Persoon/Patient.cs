namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Patient(string rijksregisterNummer, string naam, string voornaam, HashSet<Afdeling> afdelingen) 
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public HashSet<string> Afdelingen { get; } = afdelingen;

}
