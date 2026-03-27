namespace CustomMethods;

public class UserInputHulpKlasse
{
    public static double GeefKommaGetal(string vraag)
    {
        double resultaat;
        do
        {
            try
            {
                Console.Write(vraag);
                resultaat = double.Parse(new string(Console.ReadLine()));

                return resultaat;
            }
            catch
            {
                Console.WriteLine("Oeps er liep iets mis. Probeer opnieuw.");
            }
        } while (true);
    }

    public static double GeefPositiefKommaGetal(string vraag)
    {
        double resultaat;
        do
        {
            resultaat = GeefKommaGetal(vraag);

            if (resultaat < 0)
            {
                Console.WriteLine("Het getal moet positief zijn. Probeer opnieuw.");
                continue;
            }

            return resultaat;
        } while (true);
    }

    public static int GeefGeheelGetal(string vraag)
    {
        int resultaat;
        do
        {
            try
            {
                Console.Write(vraag);
                resultaat = int.Parse(new string(Console.ReadLine()));

                return resultaat;
            }
            catch
            {
                Console.WriteLine("Oeps er liep iets mis. Probeer opnieuw.");
            }
        } while (true);
    }

    public static int GeefPositiefGeheelGetal(string vraag)
    {
        int resultaat;
        do
        {
            resultaat = GeefGeheelGetal(vraag);

            if (resultaat < 0)
            {
                Console.WriteLine("Het getal moet positief zijn. Probeer opnieuw.");
                continue;
            }

            return resultaat;
        } while (true);
    }

    /// <summary>
    /// Geeft een tekst A.D.H.V. User input
    /// </summary>
    /// <param name="vraag">Vraag voor de User</param>
    /// <returns>string antwoord van de User</returns>
    public static string GeefTekst(string vraag)
    {
        string resultaat;
        do
        {
            try
            {
                Console.Write(vraag);
                resultaat = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(resultaat))
                {
                    Console.WriteLine("Je moet iets ingeven. Probeer opnieuw.");
                    continue;
                }

                return resultaat;
            }
            catch
            {
                Console.WriteLine("Oeps er liep iets mis. Probeer opnieuw.");
            }
        } while (true);
    }

    /// <summary>
    /// Geeft een datum A.D.H.V. User input
    /// </summary>
    /// <param name="vraag">Vraag voor de User</param>
    /// <returns>DateOnly antwoord van de User</returns>
    public static DateOnly GeefDatum(string vraag)
    {
        DateOnly resultaat;
        do
        {
            try
            {
                Console.Write(vraag);
                resultaat = DateOnly.Parse(new string(Console.ReadLine()));
                return resultaat;
            }
            catch
            {
                Console.WriteLine("Oeps er liep iets mis. Probeer opnieuw.");
            }
        } while (true);
    }

    /// <summary>
    /// Geeft een toekomstigeDatum A.D.H.V. User input
    /// </summary>
    /// <param name="vraag">Vraag voor de User</param>
    /// <returns>DateOnly antwoord van de User</returns>
    public static DateOnly GeefToekomstigeDatum(string vraag)
    {
        DateOnly resultaat;
        do
        {
            resultaat = GeefDatum(vraag);
            if (resultaat <= DateOnly.FromDateTime(DateTime.Now))
            {
                Console.WriteLine("De datum moet in de toekomst liggen. Probeer opnieuw.");
                continue;
            }
            return resultaat;
        } while (true);
    }

    /// <summary>
    /// Geeft een tijd A.D.H.V. User input
    /// </summary>
    /// <param name="vraag">Vraag voor de User</param>
    /// <returns>TimeOnly antwoord van de User</returns>
    public static TimeOnly GeefTijd(string vraag)
    {
        TimeOnly resultaat;
        do
        {
            try
            {
                Console.Write(vraag);
                resultaat = TimeOnly.Parse(new string(Console.ReadLine()));
                return resultaat;
            }
            catch
            {
                Console.WriteLine("Oeps er liep iets mis. Probeer opnieuw.");
            }
        } while (true);
    }

    /// <summary>
    /// Vraagt een getal binnen het bereik aan de User
    /// </summary>
    /// <param name="vraag">De vraag die gestelt wordt aan de User</param>
    /// <param name="min">Het minimum van het bereik</param>
    /// <param name="max">Het maximum van het bereik</param>
    /// <returns>De keuze van de User</returns>
    public static int VraagGetalBinnenBereik(string vraag, int min, int max)
    {
        int ant;
        while (true)
        {
            try
            {
                Console.Write($"{vraag}\n");
                try
                {
                    ant = int.Parse(Console.ReadLine() ?? throw new ArgumentException("Dit is geen geldig getal"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                if (ant < min || ant > max)
                {
                    Console.WriteLine($"Keuze moet in het interval [{min},{max}] liggen");
                    continue;
                }
                break;
            }
            catch
            {
                Console.WriteLine($"Keuze moet in het interval [{min},{max}] liggen");
            }
        }
        return ant;
    }

    /// <summary>
    /// Toont een titel van tekst
    /// </summary>
    /// <param name="titel">De titel</param>
    public static void ToonTitel(string titel)
    {
        Console.WriteLine(titel);
        Console.WriteLine(new string('-', titel.Length));
    }

    /// <summary>
    /// Vraagt de User om een keuze te maken uit een menu met opties!
    /// </summary>
    /// <param name="titel">De titel voor het menu</param>
    /// <param name="opties">De opties die gelijst worden onder de titel</param>
    /// <param name="vraag">De vraag die gesteld word aan de User</param>
    /// <returns>Geeft de keuze van de User terug</returns>
    public static int MaakKeuzeUitMenu(string titel, List<string> opties, string vraag)
    {
        ToonTitel(titel);

        int counter = 0;

        foreach (var optie in opties)
        {
            Console.WriteLine($"{counter++}. {optie}");
        }

        return VraagGetalBinnenBereik(vraag, 0, opties.Count - 1);
    }
}
