namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal class Operatie(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, Afdeling afdelingCode, string beschrijving, int aantalMinutenOpVoorhand) : Afspraak(id, datumTijdStart, duurMin, rijksregisterNrPatiënt, rijksregisterNrArts, afdelingCode)
{
    public string Beschrijving { get; } = beschrijving;

    public int AantalMinutenOpVoorhand { get; } = aantalMinutenOpVoorhand;

    public override string ToString()
    {
        return base.ToString() + $";{Beschrijving};{AantalMinutenOpVoorhand}";
    }
}
