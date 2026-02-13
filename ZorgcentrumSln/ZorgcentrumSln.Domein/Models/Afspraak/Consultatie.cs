namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal class Consultatie(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, Afdeling afdelingCode) : Afspraak(id, datumTijdStart, duurMin, rijksregisterNrPatiënt, rijksregisterNrArts, afdelingCode)
{
}
