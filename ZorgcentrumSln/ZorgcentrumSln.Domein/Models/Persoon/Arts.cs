namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Arts(string rijksregisterNummer, string naam, string voornaam, string riziv, string specialisatie, HashSet<string> afdelingen)
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public string RIZIV { get; } = riziv;
    public string Specialisatie { get; } = specialisatie;

    public HashSet<string> Afdelingen { get; } = afdelingen;
}
