namespace ZorgcentrumSln.Domein.Dtos;

public abstract record AfspraakDto(int Id, DateTime DatumTijdStart, int duurMin, string RijksregisterNrPatient, string RijksregisterNrArts, string afdelingCode);

public record ConsultatieAfspraakDto(int Id, DateTime DatumTijdStart, int duurMin, string RijksregisterNrPatient, string RijksregisterNrArts, string afdelingCode) 
    : AfspraakDto(Id, DatumTijdStart, duurMin, RijksregisterNrPatient, RijksregisterNrArts, afdelingCode);

public record ControleAfspraakDto(int Id, DateTime DatumTijdStart, int duurMin, string RijksregisterNrPatient, string RijksregisterNrArts, 
    string afdelingCode, int Referentie) 
    : AfspraakDto(Id, DatumTijdStart, duurMin, RijksregisterNrPatient, RijksregisterNrArts, afdelingCode);

public record OperatieAfspraakDto
    (int Id, DateTime DatumTijdStart, int duurMin, string RijksregisterNrPatient, string RijksregisterNrArts, 
    string afdelingCode, string Beschrijving, int AantalMinutenOpVoorhand)
    : AfspraakDto(Id, DatumTijdStart, duurMin, RijksregisterNrPatient, RijksregisterNrArts, afdelingCode);