using System.Data;
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

    protected static readonly List<string> AfdelingCodes = [
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
        return obj is Persoon persoon && persoon.RijksregisterNummer == RijksregisterNummer;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RijksregisterNummer);
    }

    protected static string CheckRijksregisterNummer(string rijksregisterNummer)
    {
        string nummer = rijksregisterNummer.Trim().Replace(".", "").Replace("-", "");

        if (nummer.Length != 11)
        {
            throw new ArgumentOutOfRangeException(nameof(rijksregisterNummer), $"Het nummer {rijksregisterNummer} is te kort of te lang");
        }

        string controleGetal = nummer[9..];
        string getal = nummer[..9];

        try
        {
            long longGetal = long.Parse(getal);
            int intControleGetal = int.Parse(controleGetal);

            if ((97 - (longGetal % 97)) == intControleGetal)
            {
                return rijksregisterNummer;
            }
            else
            {
                longGetal = long.Parse("2" + getal);
                if ((97 - (longGetal % 97)) == intControleGetal)
                {
                    return rijksregisterNummer;
                }

                throw new ArgumentException($"Het nummer {rijksregisterNummer} is niet correct");
            }
        }
        catch (Exception e)
        {
            throw new PersoonException("Het Rijksregisternummer is niet correct", e);
        }
    }

    protected static List<string> CheckAfdelingCodes(HashSet<string> value)
    {
        if(value == null)
        {
            throw new PersoonException("Er liep iets fout met het maken van deze persoon", new ArgumentNullException(nameof(value), "Parameter was null"));
        }

        List<string> afdelingen = [];

        foreach (var item in value)
        {
            try
            {
                string line;
                if (string.IsNullOrWhiteSpace(item))
                {
                    throw new ArgumentNullException(nameof(value), "Alle waarden in de collectie mogen niet null of leeg zijn");
                }
                else if (!AfdelingCodes.Contains(line = item.Replace("}", "").Replace("{", "")))
                {
                    var data = line.Split(',');

                    foreach (var code in data)
                    {
                        if (AfdelingCodes.Contains(code))
                        {
                            if (!afdelingen.Contains(code))
                            {
                                afdelingen.Add(code);
                            }

                            continue;
                        }

                        throw new ArgumentOutOfRangeException(nameof(value), $"Dit is geen geldige waarde {item}");
                    }
                }
                else if (AfdelingCodes.Contains(line))
                {
                    if (!afdelingen.Contains(line))
                    {
                        afdelingen.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                throw new PersoonException("Er liep iets fout met het maken van deze persoon", e);
            }
        }

        return afdelingen;
    }
}
