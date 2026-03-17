using ZorgcentrumSln.Domein.Exceptions;
using ZorgcentrumSln.Domein.Models.ZorgCentrumMotherClass;

namespace ZorgcentrumSln.Domein.Models.Afspraak;

internal abstract class Afspraak : ZorgCentrumObj
{
    //AfspraakId;Type;DatumTijdStart;DuurMin;RijksregisterNrPatiënt;RijksregisterNrArts;AfdelingCode; Extra1;Extra2
    public Afspraak(int id, DateTime datumTijdStart, int duurMin, string rijksregisterNrPatient, string rijksregisterNrArts, string afdelingCode)
    {
        Id = id;
        DatumTijdStart = datumTijdStart;
        DuurMin = duurMin;
        RijksregisterNrPatient = rijksregisterNrPatient;
        RijksregisterNrArts = rijksregisterNrArts;
        AfdelingCode = afdelingCode;
    }

    public int Id { get; init; }
    public DateTime DatumTijdStart { get; init; }
    public virtual int DuurMin { get; init; }
    public string RijksregisterNrPatient 
    { 
        get; init
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("waarde is leeg of null");
                }
                else
                {
                    if((RijksregisterNrArts != null && RijksregisterNrArts != value) || RijksregisterNrArts == null)
                    {
                        field = value;
                    }
                    else
                        throw new ArgumentException("Hetzelfde rijksregister mag niet bij patient en arts staan");
                }
            }
            catch (Exception e)
            {
                throw new AfspraakException("Er liep iets mis bij het maken van de afspraak", e);
            }
        } 
    }
    public string RijksregisterNrArts
    {
        get; init
        {
            try 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("waarde is leeg of null");
                }
                else
                {
                    if ((RijksregisterNrArts != null && RijksregisterNrArts != value) || RijksregisterNrPatient == null)
                    {
                        field = value;
                    }
                    else
                        throw new ArgumentException("Hetzelfde rijksregister mag niet bij patient en arts staan");
                }
            }
            catch (Exception e)
            {
                throw new AfspraakException("Er liep iets mis bij het maken van de afspraak", e);
            }
        }
    }
    public string AfdelingCode
    {
        get; init
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("waarde is leeg of null");
                }
                else
                {
                    if (AfdelingCodes.Contains(value))
                        field = value;
                    else
                        throw new ArgumentOutOfRangeException(nameof(value), $"Deze afdeling code {value} zit niet in de lijst van codes");
                }
            }
            catch (Exception e)
            {
                throw new AfspraakException("Er liep iets mis bij het maken van de afspraak", e);
            }
        }
    }
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