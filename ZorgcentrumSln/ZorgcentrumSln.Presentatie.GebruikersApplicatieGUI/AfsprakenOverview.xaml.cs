using System.Windows;
using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Domein.Dtos;
using ZorgcentrumSln.Domein.Models;

namespace ZorgcentrumSln.Presentatie.GebruikersApplicatieGUI;
/// <summary>
/// Interaction logic for AfsprakenOverview.xaml
/// </summary>
public partial class AfsprakenOverview : Window
{
    private bool _isAdmin;
    private DomeinController _dc;

    public AfsprakenOverview(bool isAdmin, DomeinController dc)
    {
        _dc = dc;
        _isAdmin = isAdmin;

        InitializeComponent();

        RefreshAfsprakenListView();
        RefreshFilterItems();

        if (!_isAdmin)
            // Disable or hide admin-specific controls
            AdminButtonsPanel.Visibility = Visibility.Collapsed;
        else
            AdminButtonsPanel.Visibility = Visibility.Visible;
    }

    private void RefreshFilterItems()
    {
        FilterArtsenComboBox.ItemsSource = _dc.GetAllArtsen().ToList().OrderBy(x => x.Naam);
        FilterPatientenComboBox.ItemsSource = _dc.GetAllPatienten().OrderBy(x => x.Naam);

        List<Afdeling> afdelingen = new List<Afdeling>();

        foreach (var item in _dc.GetAllAfdelingCodes())
        {
            Afdeling afdeling = new Afdeling(item);
            afdelingen.Add(afdeling);
        }

        FilterAfdelingenComboBox.ItemsSource = afdelingen.OrderBy(a => a.Naam);
    }

    private void RefreshAfsprakenListView()
    {
        AfsprakenListView.ItemsSource = _dc.GetAllAfspraken().OrderBy(x => x.Id);
    }

    private void AfsluitenMenuItem_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
    {
        // Add logic to apply filters based on user input and refresh the list view
        // Give an easy to implement filter format to integrate into the database query
        string filter = "";
        if(FilterArtsenComboBox.SelectedIndex != -1)
            filter += $"ar.RijksregisterNummer='{(FilterArtsenComboBox.SelectedItem as ArtsPersoonDto).RijksregisterNummer}'";

        if (FilterPatientenComboBox.SelectedIndex != -1)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter += " AND ";
            }
            filter += $"p.RijksregisterNummer='{(FilterPatientenComboBox.SelectedItem as PatientPersoonDto).RijksregisterNummer}'"; 
        }

        if (FilterAfdelingenComboBox.SelectedIndex != -1)
        {
            if (!string.IsNullOrWhiteSpace(filter) && filter.Substring(filter.Length - 4, 4) != "AND ")
            {
                filter += " AND ";
            }
            filter += $"a.AfdelingCode='{(FilterAfdelingenComboBox.SelectedItem as Afdeling).Code}'"; 
        }

        if(!string.IsNullOrWhiteSpace(filter))
            AfsprakenListView.ItemsSource = _dc.GetAfsprakenByFilter(filter).OrderBy(x => x.Id).ToList();
        else
            MessageBox.Show("Selecteer minstens één filter om toe te passen.", "Geen filter geselecteerd", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
    {
        RefreshAfsprakenListView();
        FilterAfdelingenComboBox.SelectedIndex = -1;
        FilterPatientenComboBox.SelectedIndex = -1;
        FilterArtsenComboBox.SelectedIndex = -1;
    }

    private void AfsprakenListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (AfsprakenListView.SelectedItem is AfspraakDto selectedAfspraak)
        {
            var afspraakDetailsWindow = new AfspraakDetails(_dc, AfspraakDetails.AfspraakDetailsMode.View, selectedAfspraak);
            afspraakDetailsWindow.ShowDialog();

            RefreshAfsprakenListView();
        }
    }

    private void AfspraakToevoegenButton_Click(object sender, RoutedEventArgs e)
    {
        var afspraakDetailsWindow = new AfspraakDetails(_dc, AfspraakDetails.AfspraakDetailsMode.Create);
        afspraakDetailsWindow.ShowDialog();

        RefreshAfsprakenListView();
    }

    private void AfspraakBewerkenButton_Click(object sender, RoutedEventArgs e)
    {
        if (AfsprakenListView.SelectedItem is AfspraakDto selectedAfspraak)
        {
            var afspraakDetailsWindow = new AfspraakDetails(_dc, AfspraakDetails.AfspraakDetailsMode.Edit, selectedAfspraak);
            afspraakDetailsWindow.ShowDialog(); 

            RefreshAfsprakenListView();
        }
    }

    private void AfspraakVerwijderenButton_Click(object sender, RoutedEventArgs e)
    {
        if(AfsprakenListView.SelectedItem is AfspraakDto selectedAfspraak)
        {
            var result = MessageBox.Show($"Weet u zeker dat u afspraak {selectedAfspraak.Id} wilt verwijderen?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _dc.DeleteAfspraak(selectedAfspraak.Id);
                RefreshAfsprakenListView();
            }
        }
    }

    private void PatientToevoegenButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void PatientBeherenButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
