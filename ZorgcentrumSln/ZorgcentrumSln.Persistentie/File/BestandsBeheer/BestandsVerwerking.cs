using System.Reflection.Metadata.Ecma335;
using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Persistentie.File.Factories;

namespace ZorgcentrumSln.Persistentie.File.BestandsBeheer;

internal static class BestandsVerwerking
{
    internal static List<PersoonDto> LeesBestanden(string padNaarArtsBestand, string padNaarPatientBestand)
    {
        List<PersoonDto> personen = [];

		try
		{
			using (var reader = new StreamReader(padNaarArtsBestand))
			{
				string? line;

				while ((line = reader.ReadLine()) != null)
				{
					PersoonDto newPersoon = PersoonDtoFactory.MapToPersoonDto(line);

					personen.Add(newPersoon);
				}
			}
		}
		catch (Exception e)
		{
			throw;
		}

        return personen;
    }
}