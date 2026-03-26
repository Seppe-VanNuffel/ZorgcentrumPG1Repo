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
/// Interaction logic for AfspraakDetails.xaml
/// </summary>
public partial class AfspraakDetails : Window
{
    public enum AfspraakDetailsMode
    {
        Create,
        Edit,
        View
    }

    private const string ConsultatieType = "Consultatie";
    private const string OperatieType = "Operatie";
    private const string ControleType = "Controle";

    private AfspraakDto _afspraak;
    private DomeinController _dc;
    private AfspraakDetailsMode _mode;

    public AfspraakDetails(DomeinController dc, AfspraakDetailsMode detailsMode, AfspraakDto afspraak = null)
    {
        InitializeComponent();

        if(afspraak is null && detailsMode != AfspraakDetailsMode.Create)
        {
            throw new ArgumentException("Afspraak mag niet null zijn bij Edit of View mode.");
        }

        _afspraak = afspraak;
        _dc = dc;
        _mode = detailsMode;

        switch (_mode)
        {
            case AfspraakDetailsMode.Create:
                Title = "Nieuwe Afspraak";
                break;
            case AfspraakDetailsMode.Edit:
                Title = "Afspraak Bewerken";
                break;
            case AfspraakDetailsMode.View:
                Title = "Afspraak Details";
                ButtonStackPanel.Visibility = Visibility.Collapsed;
                // Disable all input controls
                IDTextBox.IsEnabled = false;
                AfdelingTextBox.IsEnabled = false;
                DatumTijdDatePicker.IsEnabled = false;
                DuurTextBox.IsEnabled = false;
                ArtsTextBox.IsEnabled = false;
                PatientTextBox.IsEnabled = false;
                TypeComboBox.IsEnabled = false;
                AantalMinutenTextBox.IsEnabled = false;
                BeschrijvingTextBox.IsEnabled = false;
                ReferentieTextBox.IsEnabled = false;
                break;
        }

        TypeComboBox.ItemsSource = new List<string> { ConsultatieType, OperatieType, ControleType };

        if (_afspraak is not null)
        {
            IDTextBox.Text = _afspraak.Id.ToString();
            AfdelingTextBox.Text = _afspraak.afdelingCode;
            DatumTijdDatePicker.SelectedDate = _afspraak.DatumTijdStart;
            TijdUurComboBox.SelectedIndex = _afspraak.DatumTijdStart.Hour;
            TijdMinutenComboBox.SelectedIndex = _afspraak.DatumTijdStart.Minute / 5;
            DuurTextBox.Text = _afspraak.duurMin.ToString();
            ArtsTextBox.Text = _afspraak.RijksregisterNrArts;
            PatientTextBox.Text = _afspraak.RijksregisterNrPatient;
            switch (_afspraak)
            {
                case OperatieAfspraakDto operatie:
                    TypeComboBox.SelectedItem = OperatieType;
                    AantalMinutenTextBox.Text = operatie.AantalMinutenOpVoorhand.ToString();
                    BeschrijvingTextBox.Text = operatie.Beschrijving;
                    break;
                case ConsultatieAfspraakDto consultatie:
                    TypeComboBox.SelectedItem = ConsultatieType;
                    break;
                case ControleAfspraakDto controle:
                    TypeComboBox.SelectedItem = ControleType;
                    ReferentieTextBox.Text = controle.Referentie.ToString();
                    break;
                default:
                    throw new NotImplementedException("Onbekend afspraak type.");
            
            }
        }

        SetStackPanelVisibility();
    }

    private void SetStackPanelVisibility()
    {
        if(TypeComboBox.SelectedIndex == -1)
        {
            AantalMinutenStackPanel.Visibility = Visibility.Collapsed;
            BeschrijvingStackPanel.Visibility = Visibility.Collapsed;
            ReferentieStackPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
            switch (TypeComboBox.SelectedItem)
            {
                case OperatieType:
                    AantalMinutenStackPanel.Visibility = Visibility.Visible;
                    BeschrijvingStackPanel.Visibility = Visibility.Visible;
                    ReferentieStackPanel.Visibility = Visibility.Collapsed;
                    break;
                case ConsultatieType:
                    AantalMinutenStackPanel.Visibility = Visibility.Collapsed;
                    BeschrijvingStackPanel.Visibility = Visibility.Collapsed;
                    ReferentieStackPanel.Visibility = Visibility.Collapsed;
                    break;
                case ControleType:
                    AantalMinutenStackPanel.Visibility = Visibility.Collapsed;
                    BeschrijvingStackPanel.Visibility = Visibility.Collapsed;
                    ReferentieStackPanel.Visibility = Visibility.Visible;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }

    private void OpslaanButton_Click(object sender, RoutedEventArgs e)
    {
        AfspraakDto afspraak;

        switch (_mode)
        {
            case AfspraakDetailsMode.Create:
                afspraak = MapToAfspraakDto() ?? throw new InvalidOperationException("Ongeldige afspraak gegevens.");
                _dc.InsertAfspraak(afspraak);
                break;
            case AfspraakDetailsMode.Edit:
                afspraak = MapToAfspraakDto() ?? throw new InvalidOperationException("Ongeldige afspraak gegevens.");
                _dc.UpdateAfspraak(afspraak);
                break;


            case AfspraakDetailsMode.View:
            default:
                break;
        }
    }

    private AfspraakDto? MapToAfspraakDto()
    {
        if (TypeComboBox.SelectedItem is null)
        {
            MessageBox.Show("Selecteer een afspraak type.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        if(DatumTijdDatePicker.SelectedDate is null)
        {
            MessageBox.Show("Selecteer een datum.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        AfspraakDto afspraak;

        int id = int.MaxValue;
        string afdelingCode = AfdelingTextBox.Text;
        DateTime dateTime = DatumTijdDatePicker.SelectedDate.Value;
        dateTime = dateTime.AddHours(TijdUurComboBox.SelectedIndex);
        dateTime = dateTime.AddMinutes(TijdMinutenComboBox.SelectedIndex * 5);
        int duurmin = int.Parse(DuurTextBox.Text);
        string rijksregisterNrArts = ArtsTextBox.Text;
        string rijksregisterNrPatient = PatientTextBox.Text;

        if (TypeComboBox.SelectedItem.ToString() == OperatieType)
        {
            int aantalMinutenOpVoorhand = int.Parse(AantalMinutenTextBox.Text);
            string beschrijving = BeschrijvingTextBox.Text;
            afspraak = new OperatieAfspraakDto(id, dateTime, duurmin, rijksregisterNrPatient, rijksregisterNrArts, afdelingCode, beschrijving, aantalMinutenOpVoorhand);
        }
        else if (TypeComboBox.SelectedItem.ToString() == ControleType)
        {
            int referentie = int.Parse(ReferentieTextBox.Text);
            afspraak = new ControleAfspraakDto(id, dateTime, duurmin, rijksregisterNrPatient, rijksregisterNrArts, afdelingCode, referentie);
        }
        else if (TypeComboBox.SelectedItem.ToString() == ConsultatieType)
        {
            afspraak = new ConsultatieAfspraakDto(id, dateTime, duurmin, rijksregisterNrPatient, rijksregisterNrArts, afdelingCode);
        }
        else
            return null;

        return afspraak;
    }

    private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SetStackPanelVisibility();
    }

    private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
