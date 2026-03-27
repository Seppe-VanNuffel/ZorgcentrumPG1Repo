using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZorgcentrumSln.Domein;
using ZorgcentrumSln.Domein.Dtos;

namespace ZorgcentrumSln.Presentatie.GebruikersApplicatieGUI;
/// <summary>
/// Interaction logic for PatientenDetails.xaml
/// </summary>
public partial class PatientenDetails : Window
{
    public enum PatientenDetailsMode
    {
        Create,
        Edit
    }

    private PatientenDetailsMode _mode;
    private DomeinController _dc;

    public PatientenDetails(PatientenDetailsMode mode, DomeinController dc)
    {
        InitializeComponent();

        _mode = mode;
        _dc = dc;

        foreach (var afdeling in _dc.GetAllAfdelingCodes())
        {
            CheckBox checkBox = new CheckBox();
            checkBox.IsChecked = false;
            checkBox.Content = afdeling;
            AfdelingenGroupBoxStackPanel.Children.Add(checkBox);
        }

        switch (_mode)
        {
            case PatientenDetailsMode.Create:
                PatientSelectorStackPanel.Visibility = Visibility.Collapsed;
                break;
            case PatientenDetailsMode.Edit:
                PatientSelectorStackPanel.Visibility = Visibility.Visible;
                PatientSelectorComboBox.ItemsSource = _dc.GetAllPatienten();
                break;
            default:
                break;
        }
    }

    private void OpslaanButton_Click(object sender, RoutedEventArgs e)
    {
        PersoonDto persoon;

        try
        {
            switch (_mode)
            {
                case PatientenDetailsMode.Create:
                    persoon = MapToPatientDto();
                    _dc.InsertPersoon(persoon);
                    break;
                case PatientenDetailsMode.Edit:
                    persoon = MapToPatientDto();
                    _dc.UpdatePersoon(persoon);
                    break;
                default:
                    break;
            }

            PatientSelectorComboBox.ItemsSource = _dc.GetAllPatienten();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Er liep iets mis\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private PersoonDto MapToPatientDto()
    {
        string rijksregisterNummer = RijksregisternummerTextBox.Text;
        string naam = NaamTextBox.Text;
        string voornaam = VoornaamTextBox.Text;
        HashSet<string> afdelingen = new HashSet<string>();

        foreach (CheckBox checkBox in AfdelingenGroupBoxStackPanel.Children)
        {
            if(checkBox.IsChecked == true)
            {
                afdelingen.Add(checkBox.Content as string);
            }
        }

        return new PatientPersoonDto(rijksregisterNummer, naam, voornaam, afdelingen);
    }

    private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void PatientSelectorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var patient = PatientSelectorComboBox.SelectedItem as PatientPersoonDto;

            RijksregisternummerTextBox.Text = patient.RijksregisterNummer;
            NaamTextBox.Text = patient.Naam;
            VoornaamTextBox.Text = patient.Voornaam;

            foreach (CheckBox checkBox in AfdelingenGroupBoxStackPanel.Children)
            {
                if (patient.Afdelingen.Contains(checkBox.Content as string))
                {
                    checkBox.IsChecked = true;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Er liep iets mis {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
