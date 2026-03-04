using ZorgcentrumSln.Domein.Exceptions;

namespace ZorgcentrumSln.Domein.Models.Persoon;

internal abstract class Persoon
{
    public Persoon(string rijksregisterNummer, string naam, string voornaam)
    {
        RijksregisterNummer = rijksregisterNummer;
        Naam = naam;
        Voornaam = voornaam;
    }

    protected static List<string> AfdelingCodes = [
        "PNEC",
        "CARD",
        "GAST",
        "NEUR",
        "ORTH",
        "GERC",
        "ONCO",
        "PAAN",
        "RADI",
        "URGE"
        ];

    public string RijksregisterNummer { get => field; 
        init {
            try
            {
                field = CheckRijksregisterNummer(value);
            }
            catch (Exception e)
            {
                throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
            }
        }
    }

    public string Naam { get; 
        init
        {
            try
            {
                field = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(value), "Naam mag niet null of leeg zijn") : value;
            }
            catch (Exception e)
            {
                throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
            }
        }
    }

    public string Voornaam { get;
        init
        {
            try
            {
                field = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(value), "Voornaam mag niet null of leeg zijn") : value;
            }
            catch (Exception e)
            {
                throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
            }
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Persoon persoon && persoon.RijksregisterNummer == RijksregisterNummer && persoon.GetType().Name == GetType().Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RijksregisterNummer, GetType().Name);
    }

    internal static string CheckRijksregisterNummer(string rijksregisterNummer)
    {
        string nummer = rijksregisterNummer.Trim().Replace(".", "").Replace("-", "");

        if (nummer.Length != 11)
        {
            throw new ArgumentOutOfRangeException(nameof(rijksregisterNummer), "Het nummer is te kort of te lang");
        }

        string controleGetal = nummer[9..];
        string getal = nummer[..9];

        try
        {
            long longGetal = long.Parse(getal);
            int intControleGetal = int.Parse(controleGetal);

            if (97 - (longGetal % 97) == intControleGetal)
            {
                return rijksregisterNummer;
            }
            else
                throw new ArgumentException("Het nummer is niet correct");
        }
        catch (ArgumentException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Het nummer is niet correct", e);
        }
    }
}
