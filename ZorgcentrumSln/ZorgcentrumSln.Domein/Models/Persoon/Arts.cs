namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Arts(string rijksregisterNummer, string naam, string voornaam, Afdeling specialisatie, HashSet<Afdeling> afdelingen)
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public Afdeling Specialisatie { get; } = specialisatie;

    public HashSet<Afdeling> Afdelingen { get; } = afdelingen;
}
