using CustomMethods;
using System;
using System.Collections.Generic;
using System.Text;
using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Models;

namespace ZorgcentrumSln.Presentatie;

public class ReaderApp(DomeinController dm)
{
    private DomeinController _dm = dm;

    public void Run()
    {
        while (true)
        {
            var output = UserInputHulpKlasse.MaakKeuzeUitMenu("BestandsDisplayer", ["Artsen.csv", "Patienten.csv", "Afspraken.csv", "Afsluiten"], "Kies een bestand om weg te schrijven naar de console: ");

            switch (output)
            {
                case 0:
                    foreach (var persoon in _dm.GetAllArtsen())
                    {
                        if (persoon is ArtsPersoonDto arts)
                            Console.WriteLine($"{arts.RijksregisterNummer}, {arts.Naam} {arts.Voornaam}, {arts.Specialisate}, {string.Join('|', arts.Afdelingen)}");
                    }
                    Console.WriteLine("\n");
                    break;
                case 1:
                    foreach (var persoon in _dm.GetAllPatienten())
                    {
                        if (persoon is PatientPersoonDto patient)
                            Console.WriteLine($"{patient.RijksregisterNummer}, {patient.Naam} {patient.Voornaam}, {string.Join('|', patient.Afdelingen)}");
                    }
                    Console.WriteLine("\n");
                    break;
                case 2:
                    foreach (var afspraak in _dm.GetAllAfspraken())
                    {
                        Console.WriteLine(afspraak);
                    }
                    Console.WriteLine("\n");
                    break;

                default:
                    return;
            }
        }
    }
}
