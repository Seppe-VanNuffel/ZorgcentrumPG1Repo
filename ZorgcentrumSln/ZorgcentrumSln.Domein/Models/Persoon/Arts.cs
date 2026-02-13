namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Arts(string rijksregisterNummer, string naam, string voornaam, Specialisatie specialisatie, HashSet<Afdeling> afdelingen)
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public Specialisatie Specialisatie { get; } = specialisatie;

    public HashSet<Afdeling> Afdelingen { get; } = afdelingen;
}
