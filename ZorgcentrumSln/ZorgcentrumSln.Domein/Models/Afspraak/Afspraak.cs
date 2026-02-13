namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal abstract class Afspraak(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatient, string rijksregisterNrArts, Afdeling afdelingCode)
{
    //AfspraakId;Type;DatumTijdStart;DuurMin;RijksregisterNrPatiënt;RijksregisterNrArts;AfdelingCode; Extra1;Extra2
    public int Id { get; } = id;
    public DateTime DatumTijdStart { get; } = datumTijdStart;
    public int DuurMin { get; } = duurMin;
    public string RijksregisterNrPatient { get; } = rijksregisterNrPatient;
    public string RijksregisterNrArts { get; } = rijksregisterNrArts;
    public Afdeling AfdelingCode { get; } = afdelingCode;

    public override string ToString()
    {
        //TODO: Verander DatumTijdStart naar ""2026-04-01T09"" formaat
        string datumTijdStartString = DatumTijdStart.ToString("yyyy-MM-ddTHH");

        return $"{Id};{this.GetType().ToString().ToUpper()};{datumTijdStartString};{DuurMin};{RijksregisterNrPatient};{RijksregisterNrArts};{AfdelingCode}";
    }
}