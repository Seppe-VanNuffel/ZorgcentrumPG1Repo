namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Patient(string rijksregisterNummer, string naam, string voornaam, HashSet<string> afdelingen) 
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public HashSet<string> Afdelingen { get;
        init
        {
            foreach (var item in value)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    throw new ArgumentNullException(nameof(item), "alle waarden in de collectie mogen niet null of leeg zijn");
                }
            }
        }
    } = afdelingen;

}
