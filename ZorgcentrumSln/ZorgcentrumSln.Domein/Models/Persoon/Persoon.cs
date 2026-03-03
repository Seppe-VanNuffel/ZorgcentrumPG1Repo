namespace ZorgcentrumSln.Domein.Models.Persoon;

internal abstract class Persoon(string rijksregisterNummer, string naam, string voornaam)
{
    public string RijksregisterNummer { get => field; 
        init {
            field = CheckRijksregisterNummer(value);
        }
    } = rijksregisterNummer;

    public string Naam { get; } = naam;

    public string Voornaam { get; } = voornaam;

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
        catch (Exception)
        {
            throw;
        }
    }
}
