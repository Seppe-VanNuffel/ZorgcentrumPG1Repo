using ZorgcentrumSln.Domein.Exceptions;

namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Patient : Persoon
{
    public Patient(string rijksregisterNummer, string naam, string voornaam, HashSet<string> afdelingen) : base(rijksregisterNummer, naam, voornaam)
    {
        Afdelingen = afdelingen;
    }

    public HashSet<string> Afdelingen
    {
        get; init => field = CheckAfdelingCodes(value).ToHashSet();
    }
}
