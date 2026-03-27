using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Persistentie.DataBase;

namespace ZorgcentrumSln.Presentatie.GebruikersApplicatieGUI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ZorgcentrumDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var gebruikersModus = new GebruikersModus(connectionString);

        gebruikersModus.Show();
    }
}