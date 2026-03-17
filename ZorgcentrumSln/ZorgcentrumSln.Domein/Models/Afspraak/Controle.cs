
namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal class Controle : Afspraak
{
    public Controle(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, string afdelingCode, int referentie) : base(id, datumTijdStart, duurMin, rijksregisterNrPatiënt, rijksregisterNrArts, afdelingCode)
    {
        Referentie = referentie;
    }

    public int Referentie { get; init; }

    public override string ToString()
    {
        return base.ToString() + $";{Referentie}";
    }
}
