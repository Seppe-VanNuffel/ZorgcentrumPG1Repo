namespace ZorgcentrumSln.Domein.Dtos;

public abstract record PersoonDto(string RijksregisterNummer, string Naam, string Voornaam);

public record PatientPersoonDto(string RijksregisterNummer, string Naam, string Voornaam, HashSet<string> Afdelingen) : PersoonDto(RijksregisterNummer, Naam, Voornaam);

public record ArtsPersoonDto(string RijksregisterNummer, string Naam, string Voornaam, string RIZIV, string Specialisatie, HashSet<string> Afdelingen) : PersoonDto(RijksregisterNummer, Naam, Voornaam);
