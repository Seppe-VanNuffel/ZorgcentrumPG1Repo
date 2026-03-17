using ZorgcentrumSln.Domein.Exceptions;

namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal class Controle : Afspraak
{
    public Controle(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, string afdelingCode, int referentie) : base(id, datumTijdStart, duurMin, rijksregisterNrPatiënt, rijksregisterNrArts, afdelingCode)
    {
        Referentie = referentie;
    }

    public int Referentie { get; 
        init
        {
            if (value >= 0)
                field = value;
            else
                throw new AfspraakException("Er liep iets mis bij het maken van deze afspraak", new ArgumentOutOfRangeException("value", "Nummer mag niet onder de nul zijn"));
        } 
    
    }

    public override string ToString()
    {
        return base.ToString() + $";{Referentie}";
    }
}
