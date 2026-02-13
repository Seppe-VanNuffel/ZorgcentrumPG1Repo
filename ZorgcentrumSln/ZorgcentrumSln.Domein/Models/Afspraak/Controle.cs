
namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal class Controle(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, Afdeling afdelingCode, string referentie) : Afspraak(id, datumTijdStart, duurMin, rijksregisterNrPatiënt, rijksregisterNrArts, afdelingCode)
{
    public string Referentie { get; set; } = referentie;
    public override string ToString()
    {
        return base.ToString() + $";{Referentie}";
    }
}
