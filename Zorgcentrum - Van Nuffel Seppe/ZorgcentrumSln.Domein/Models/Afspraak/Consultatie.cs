using ZorgcentrumSln.Domein.Exceptions;

namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal class Consultatie(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatiënt, string rijksregisterNrArts, string afdelingCode) : Afspraak(id, datumTijdStart, duurMin, rijksregisterNrPatiënt, rijksregisterNrArts, afdelingCode)
{
    public override int DuurMin
    {
        get => base.DuurMin; init
        {
            if (value >= 10)
                base.DuurMin = value;
            else
                throw new AfspraakException("Er liep iets mis bij het maken van de afspraak", new ArgumentOutOfRangeException(nameof(value),"De minimum duur is 10 minuten"));
        }
    }
}