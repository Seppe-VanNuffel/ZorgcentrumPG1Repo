namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Arts(string rijksregisterNummer, string naam, string voornaam, string riziv, string specialisatie, HashSet<string> afdelingen)
    : Persoon(rijksregisterNummer, naam, voornaam)
{
    public string RIZIV { get;
        init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(specialisatie), "Specialisatie niet null of leeg zijn");
            }
        }
    } = riziv;
    public string Specialisatie { get;
        init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(specialisatie), "Specialisatie niet null of leeg zijn");
            }
        }
    } = specialisatie;

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
