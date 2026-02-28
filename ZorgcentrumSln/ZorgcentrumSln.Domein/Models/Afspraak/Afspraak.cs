namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal abstract class Afspraak(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatient, string rijksregisterNrArts, string afdelingCode)
{
    //AfspraakId;Type;DatumTijdStart;DuurMin;RijksregisterNrPatiënt;RijksregisterNrArts;AfdelingCode; Extra1;Extra2
    public int Id { get; } = id;
    public DateTime DatumTijdStart { get; } = datumTijdStart;
    public int DuurMin { get; } = duurMin;
    public string RijksregisterNrPatient { get; } = rijksregisterNrPatient;
    public string RijksregisterNrArts { get; } = rijksregisterNrArts;
    public string AfdelingCode { get; } = afdelingCode;

    public override bool Equals(object? obj)
    {
        return obj is Afspraak afspraak &&
               Id == afspraak.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string ToString()
    {
        string datumTijdStartString = DatumTijdStart.ToString("yyyy-MM-ddTHH");

        return $"{Id};{this.GetType().ToString().ToUpper()};{datumTijdStartString};{DuurMin};{RijksregisterNrPatient};{RijksregisterNrArts};{AfdelingCode}";
    }


}