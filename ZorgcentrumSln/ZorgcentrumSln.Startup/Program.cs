using ZorgcentrumSln.Domein;
using ZorgcentrumSln.PersistentieBestand;
using ZorgcentrumSln.Presentatie;

namespace ZorgcentrumSln.Startup;

internal class Program
{
    static void Main(string[] args)
    {
        const string padNaarArtsBestand = @".\Data\Artsen.csv";
        const string padNaarPatientenBestand = @".\Data\Patienten.csv";
        const string padNaarAfsprakenBestand = @".\Data\Afspraken.csv";


        DomeinController dm = new( new PersoonRepositoryBestand(padNaarArtsBestand, padNaarPatientenBestand), new AfspraakRepositoryBestand(padNaarAfsprakenBestand));

        ReaderApp app = new(dm);
        app.Run();
    }
}
