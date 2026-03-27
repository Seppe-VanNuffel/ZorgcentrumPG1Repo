using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.PersistentieBestand.Factories;

namespace ZorgcentrumSln.PersistentieBestand.BestandsBeheer;

internal static class BestandsVerwerking
{
    const string ARTSHEADER = "RijksregisterNr;Naam;Voornaam;RIZIV;Specialisatie;Afdelingen";
    const string PATIENTHEADER = "RijksregisterNr;Naam;Voornaam;Afdelingen";
    const string AFSPRAAKHEADER = "AfspraakId;Type;DatumTijdStart;DuurMin;RijksregisterNrPatiënt;RijksregisterNrArts;AfdelingCode;Extra1;Extra2";

    internal static List<PersoonDto> LeesPersoonBestanden(string padNaarArtsBestand, string padNaarPatientBestand)
    {
        List<PersoonDto> personen = [];

		try
		{
            using var reader = new StreamReader(padNaarArtsBestand);

            string? line;
            bool firstLine = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (firstLine && line != ARTSHEADER)
                    throw new InvalidDataException("Dit is niet de juiste file!");
                else if (firstLine && line == ARTSHEADER)
                {
                    firstLine = false;
                    continue;
                }

                try
                {
                    PersoonDto newPersoon = PersoonDtoFactory.MapToPersoonDto(line);

                    personen.Add(newPersoon);
                }
                catch (Exception)
                {
                    // TODO: Add Error handling here
                    throw;
                }
            }
        }
		catch (Exception)
        {
            throw;
		}

        try
        {
            using var reader = new StreamReader(padNaarPatientBestand);

            string? line;
            bool firstLine = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (firstLine && line != PATIENTHEADER)
                    throw new InvalidDataException("Dit is niet de juiste file!");
                else if (firstLine && line == PATIENTHEADER)
                {
                    firstLine = false;
                    continue;
                }

                try
                {
                    PersoonDto newPersoon = PersoonDtoFactory.MapToPersoonDto(line);

                    personen.Add(newPersoon);
                }
                catch (Exception)
                {
                    // TODO: Add Error handling here
                    throw;
                }
            }
        }
        catch (Exception)
        {
            throw;

        }

        return personen;
    }

    internal static List<AfspraakDto> LeesAfspraakBestand(string padNaarBestand)
    {
        List<AfspraakDto> afspraken = [];

        try
        {
            using var reader = new StreamReader(padNaarBestand);

            string? line;
            bool firstLine = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (firstLine && line != AFSPRAAKHEADER)
                    throw new InvalidDataException("Dit is niet de juiste file!");
                else if (firstLine && line == AFSPRAAKHEADER)
                {
                    firstLine = false;
                    continue;
                }

                try
                {
                    AfspraakDto newAfspraak = AfspraakDtoFactory.MapToAfspraakDto(line);

                    afspraken.Add(newAfspraak);
                }
                catch (Exception)
                {
                    // TODO: Add Error handling here
                    throw;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return afspraken;
    }

    internal static void SchrijfErrorBestand(string padNaarBestand, Exception e)
    {
        try
        {
            using var writer = new StreamWriter(padNaarBestand, append: true);
            writer.WriteLine($"[{DateTime.Now}] - {e.Message}");
            writer.WriteLine(e.StackTrace);
        }
        catch (Exception)
        {
            throw new Exception("Er is een fout opgetreden bij het schrijven naar het errorbestand.");
        }
    }
}