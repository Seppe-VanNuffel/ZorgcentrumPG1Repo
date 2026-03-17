using ZorgcentrumSln.Domein.Exceptions;

namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal class Operatie : Afspraak
{
    public Operatie(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, string afdelingCode, string beschrijving, int aantalMinutenOpVoorhand) : base(id, datumTijdStart, duurMin, rijksregisterNrPatiënt, rijksregisterNrArts, afdelingCode)
    {
        Beschrijving = beschrijving;
        AantalMinutenOpVoorhand = aantalMinutenOpVoorhand;
    }

    public override int DuurMin
    {
        get => base.DuurMin; init
        {
            if (value >= 30)
                base.DuurMin = value;
            else
                throw new AfspraakException("Er liep iets mis bij het maken van de afspraak", new ArgumentOutOfRangeException(nameof(value), "De minimum duur is 30 minuten"));
        }
    }

    public string Beschrijving { get;
        init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AfspraakException("Er liep iets mis bij het maken van de afspraak", new ArgumentException("De beschrijving moet ingevuld zijn"));
            }
            else
                field = value;
        }
    }

    public int AantalMinutenOpVoorhand
    {
        get => base.DuurMin; init
        {
            if (value >= 30)
                base.DuurMin = value;
            else
                throw new AfspraakException("Er liep iets mis bij het maken van de afspraak", new ArgumentOutOfRangeException(nameof(value), "De minimum duur is 30 minuten"));
        }
    }

    public override string ToString()
    {
        return base.ToString() + $";{Beschrijving};{AantalMinutenOpVoorhand}";
    }
}
