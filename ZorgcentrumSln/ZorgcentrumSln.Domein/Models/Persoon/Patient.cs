namespace ZorgcentrumSln.Domein.Models.Persoon;
D
internal class Patient(string rijksregisterNummer, string naam, string voornaam, HashSet<Afdeling> afdelingen) 
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public HashSet<Afdeling> Afdelingen { get; } = afdelingen;

}
