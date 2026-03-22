using System.ComponentModel;
using System.Windows;
using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Presentatie.GebruikersApplicatieGUI;
/// <summary>
/// Interaction logic for AfsprakenOverview.xaml
/// </summary>
public partial class AfsprakenOverview : Window
{
    private bool _isAdmin;
    private DomeinController _dc;
    private List<AfspraakDto> _afspraken;

    public AfsprakenOverview(bool isAdmin, DomeinController dc)
    {
        _dc = dc;

        _isAdmin = isAdmin;
        _afspraken = _dc.GetAllAfspraken().ToList();

        InitializeComponent();


        AfsprakenListView.ItemsSource = _afspraken;

        if (!_isAdmin)
            // Disable or hide admin-specific controls
            AdminButtonsPanel.Visibility = Visibility.Collapsed;
        else
            AdminButtonsPanel.Visibility = Visibility.Visible;
    }

    private void AfsluitenMenuItem_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
