using System.Configuration;
using System.Data;
using System.Windows;
using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Persistentie.Bestand;
using ZorgcentrumSln.Persistentie.DataBase;
using ZorgcentrumSln.PersistentieBestand;

namespace ZorgcentrumSln.Presentatie.GegevensinitialisatieGUI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ZorgcentrumDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

    private const string padNaarArtsBestand =       @".\Data\Artsen.csv";
    private const string padNaarPatientenBestand =  @".\Data\Patienten.csv";
    private const string padNaarAfsprakenBestand =  @".\Data\Afspraken.csv";
    private readonly string padNaarErrorLogBestand =   $@".\Data\ErrorLog{DateTime.Now:ddMMyyyy}.txt";

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        AfspraakRepositoryBestand afspraakRepositoryBestand = new(padNaarAfsprakenBestand);
        PersoonRepositoryBestand persoonRepositoryBestand = new(padNaarArtsBestand, padNaarPatientenBestand);
        AfspraakRepositoryDatabase afspraakRepositoryDatabase = new(connectionString);
        PersoonRepositoryDatabase persoonRepositoryDatabase = new(connectionString);
        ErrorLoggerBestand errorLoggerBestand = new(padNaarErrorLogBestand);

        var domainController = new GegevensInitDomeinController(persoonRepositoryBestand, afspraakRepositoryBestand, persoonRepositoryDatabase, afspraakRepositoryDatabase, errorLoggerBestand);

        MainWindow window = new(domainController);

        window.Show();
    }
}

