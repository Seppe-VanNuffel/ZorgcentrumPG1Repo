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

        RefreshFilterAfdelingenComboBox();

        if (!_isAdmin)
            // Disable or hide admin-specific controls
            AdminButtonsPanel.Visibility = Visibility.Collapsed;
        else
            AdminButtonsPanel.Visibility = Visibility.Visible;
    }

    private void RefreshFilterItems()
    {
        FilterArtsenComboBox.ItemsSource = _dc.GetAllArtsen().ToList().OrderBy(x => x.Naam);

        RefreshFilterPatientenComboBox();
        RefreshFilterAfdelingenComboBox();
    }

    private void RefreshFilterAfdelingenComboBox()
    {
        List<Afdeling> afdelingen = new List<Afdeling>();

        foreach (var item in _dc.GetAllAfdelingCodes())
        {
            Afdeling afdeling = new Afdeling(item);
            afdelingen.Add(afdeling);
        }

        FilterAfdelingenComboBox.ItemsSource = afdelingen.OrderBy(a => a.Naam);
    }

    private void RefreshFilterPatientenComboBox()
    {
        FilterPatientenComboBox.ItemsSource = _dc.GetAllPatienten().OrderBy(x => x.Naam);
    }

    private void AfsluitenMenuItem_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
    {
        // Add logic to apply filters based on user input and refresh the list view
        // Give an easy to implement filter format to integrate into the database query
        if(AfsprakenListView.Visibility == Visibility.Collapsed)
        {
            AfsprakenListView.Visibility = Visibility.Visible;
            NoAfsprakenTextBlock.Visibility = Visibility.Collapsed;
        }
        RefreshAfsprakenList();
    }

    private void RefreshAfsprakenList()
    {
        string filter = "";
        if (FilterArtsenComboBox.SelectedIndex != -1)
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

        if (!string.IsNullOrWhiteSpace(filter))
            AfsprakenListView.ItemsSource = _dc.GetAfsprakenByFilter(filter).OrderBy(x => x.Id).ToList();
        else
            MessageBox.Show("Selecteer minstens één filter om toe te passen.", "Geen filter geselecteerd", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
    {
        RefreshAfsprakenList();
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

            RefreshAfsprakenList();
        }
    }

    private void AfspraakToevoegenButton_Click(object sender, RoutedEventArgs e)
    {
        var afspraakDetailsWindow = new AfspraakDetails(_dc, AfspraakDetails.AfspraakDetailsMode.Create);
        afspraakDetailsWindow.ShowDialog();

        RefreshAfsprakenList();
    }

    private void AfspraakBewerkenButton_Click(object sender, RoutedEventArgs e)
    {
        if (AfsprakenListView.SelectedItem is AfspraakDto selectedAfspraak)
        {
            var afspraakDetailsWindow = new AfspraakDetails(_dc, AfspraakDetails.AfspraakDetailsMode.Edit, selectedAfspraak);
            afspraakDetailsWindow.ShowDialog(); 

            RefreshAfsprakenList();
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
                RefreshAfsprakenList();
            }
        }
    }

    private void PatientToevoegenButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new PatientenDetails(PatientenDetails.PatientenDetailsMode.Create, _dc);
        window.ShowDialog();

        RefreshFilterPatientenComboBox();
    }

    private void PatientBeherenButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new PatientenDetails(PatientenDetails.PatientenDetailsMode.Edit, _dc);
        window.ShowDialog();

        RefreshFilterPatientenComboBox();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        App.Current.Shutdown();
    }

    private void FilterAfdelingenComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        string filter = "";

        if (FilterAfdelingenComboBox.SelectedIndex != -1)
        {
            filter += $"af.AfdelingCode='{(FilterAfdelingenComboBox.SelectedItem as Afdeling).Code}'";
        }
        else
            throw new ArgumentException("Ongeldige selectie in FilterAfdelingenComboBox");

        var personen = _dc.GetAllPersonenByFilter(filter);

        if (personen != null)
        {
            FilterArtsenComboBox.ItemsSource = personen.Where(p => p is ArtsPersoonDto).OrderBy(x => x.Naam);
            FilterPatientenComboBox.ItemsSource = personen.Where(p => p is PatientPersoonDto).OrderBy(x => x.Naam);
        }
    }

    private void FilterArtsenComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        string filter = "";

        if (FilterArtsenComboBox.SelectedIndex != -1)
            filter += $"afspr.RijksregisterNrArts='{(FilterArtsenComboBox.SelectedItem as ArtsPersoonDto).RijksregisterNummer}'";
        else
            throw new ArgumentException("Ongeldige selectie in FilterArtsenComboBox");


        if (FilterAfdelingenComboBox.SelectedIndex != -1)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter += " AND ";
            }
            filter += $"af.AfdelingCode='{(FilterAfdelingenComboBox.SelectedItem as Afdeling).Code}'";
        }
        else
            throw new ArgumentException("Ongeldige selectie in FilterAfdelingenComboBox");

        var personen = _dc.GetAllPatientenByFilter(filter);

        if (personen != null)
        {
            FilterPatientenComboBox.ItemsSource = personen.OrderBy(x => x.Naam);
        }
    }
}
