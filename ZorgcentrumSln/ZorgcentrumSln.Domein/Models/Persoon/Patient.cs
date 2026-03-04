using ZorgcentrumSln.Domein.Exceptions;

namespace ZorgcentrumSln.Domein.Models.Persoon;

internal class Patient : Persoon
{
    public Patient(string rijksregisterNummer, string naam, string voornaam, HashSet<string> afdelingen) : base(rijksregisterNummer, naam, voornaam)
    {
        Afdelingen = afdelingen;
    }

    public HashSet<string> Afdelingen { get;
        init
        {
            foreach (var item in value)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        throw new ArgumentNullException(nameof(value), "Alle waarden in de collectie mogen niet null of leeg zijn");
                    }
                    else if (!AfdelingCodes.Contains(item.Replace("}", "").Replace("{", "")))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), "Dit is geen geldige waarde");
                    }
                }
                catch (Exception e)
                {
                    throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
                }
            }

            field = value;
        }
    }
}
