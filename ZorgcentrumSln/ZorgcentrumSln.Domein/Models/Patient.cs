namespace ZorgcentrumSln.Domein.Models;

internal class Patient(string rijksregisterNummer, string naam, string voornaam) : Persoon(rijksregisterNummer, naam, voornaam)
{
}
