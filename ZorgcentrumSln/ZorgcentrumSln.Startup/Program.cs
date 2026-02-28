using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Persistentie.File;
using ZorgcentrumSln.Presentatie;

namespace ZorgcentrumSln.Startup;

internal class Program
{
    static void Main(string[] args)
    {
        DomeinController dm = new(new PersoonRepositoryBestand(@".\Data\Artsen.csv", @".\Data\Patienten.csv"));

        ReaderApp app = new ReaderApp(dm);
        app.Run();
    }
}
