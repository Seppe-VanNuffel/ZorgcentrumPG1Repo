using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Persistentie.DataBase;

namespace ZorgcentrumSln.Presentatie.GebruikersApplicatieGUI;

/// <summary>
/// Interaction logic for GebruikersModus.xaml
/// </summary>
public partial class GebruikersModus : Window
{
    private readonly string _connection;
    private DomeinController _dc;

    public GebruikersModus(string connection)
    {
        InitializeComponent();

        _connection = connection;
        AfspraakRepositoryDatabase afspraakRepositoryDatabase = new(_connection);
        PersoonRepositoryDatabase persoonRepositoryDatabase = new(_connection);

        _dc = new DomeinController(afspraakRepositoryDatabase, persoonRepositoryDatabase);
    }

    private void AdminButton_Click(object sender, RoutedEventArgs e)
    {
        var afsprakenOverview = new AfsprakenOverview(isAdmin: true, _dc);

        afsprakenOverview.Show();
        this.Close();
    }

    private void ReadOnlyButton_Click(object sender, RoutedEventArgs e)
    {
        var afsprakenOverview = new AfsprakenOverview(isAdmin: false, _dc);

        afsprakenOverview.Show();
        this.Close();
    }
}