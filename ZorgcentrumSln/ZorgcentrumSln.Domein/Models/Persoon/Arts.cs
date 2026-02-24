namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Arts(string rijksregisterNummer, string naam, string voornaam, string specialisatie, HashSet<string> afdelingen)
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public string Specialisatie { get; } = specialisatie;

    public HashSet<string> Afdelingen { get; } = afdelingen;
}
