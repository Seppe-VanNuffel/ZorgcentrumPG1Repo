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
using ZorgcentrumSln.Domein.Dtos;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace ZorgcentrumSln.Presentatie.GegevensinitialisatieGUI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private GegevensInitDomeinController _controller;

    private ObservableCollection<string> _bestanden; 
    private ObservableCollection<string> _selectedbestanden;

    public MainWindow(GegevensInitDomeinController controller)
    {
        InitializeComponent();
        _controller = controller;

        _bestanden = new ObservableCollection<string>()
        {
            "Artsen.csv",
            "Patienten.csv",
            "Afspraken.csv"
        };

        _selectedbestanden = new ObservableCollection<string>();

        // Assign ItemsSource once
        BestandenListView.ItemsSource = _bestanden;
        BestandenSelectedListView.ItemsSource = _selectedbestanden;

        MainStatusBar.Visibility = Visibility.Collapsed;
    }

    private void Select_Click(object sender, RoutedEventArgs e)
    {
        if(BestandenListView.SelectedItems.Count == 0)
        {
            MessageBox.Show("Selecteer minstens een bestand", "Opmerking", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else if(BestandenListView.SelectedItems.Count > 0)
        {
            for (int i = BestandenListView.SelectedItems.Count - 1; i >= 0; i--)
            {
                _selectedbestanden.Add(BestandenListView.SelectedItems[i].ToString());
                _bestanden.Remove(BestandenListView.SelectedItems[i].ToString());
            }

            //RefreshLists();
        }
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        if (BestandenSelectedListView.SelectedItems.Count == 0)
        {
            MessageBox.Show("Selecteer minstens een bestand", "Opmerking", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else if (BestandenSelectedListView.SelectedItems.Count > 0)
        {
            for (int i = BestandenSelectedListView.SelectedItems.Count - 1; i >= 0; i--)
            {
                _bestanden.Add(BestandenSelectedListView.SelectedItems[i].ToString());
                _selectedbestanden.Remove(BestandenSelectedListView.SelectedItems[i].ToString());
            }

            // RefreshLists();
        }
    }

    private async void Continue_Click(object sender, RoutedEventArgs e)
    {
        if (BestandenSelectedListView.Items.Count == 0)
        {
            MessageBox.Show("Selecteer minstens een bestand", "Opmerking", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            try
            {                
                if(BestandenSelectedListView.Items.Contains("Artsen.csv"))
                {
                    var artsen = _controller.GetAllArtsen();

                    FileNameTextBox.Text = "Artsen.csv";
                    FileProgressBar.Value = 0;
                    FileProgressBar.Maximum = artsen.Count;
                    MainStatusBar.Visibility = Visibility.Visible;

                    await Task.Run(() => 
                    {
                        _controller.ClearAllAfspraken();
                        _controller.ClearAllPersonen();

                        foreach (ArtsPersoonDto arts in artsen)
                        {
                            try
                            {
                                _controller.InsertPersoon(arts);
                            }
                            catch
                            {
                                Debug.WriteLine($"Fout in arts");
                            }
                            finally
                            {
                                Application.Current.Dispatcher.Invoke(() => FileProgressBar.Value++);
                            }
                        }
                    });

                    MainStatusBar.Visibility = Visibility.Collapsed;
                }

                if (BestandenSelectedListView.Items.Contains("Patienten.csv"))
                {
                    var patienten = _controller.GetAllPatienten();

                    FileNameTextBox.Text = "Patienten.csv";

                    FileProgressBar.Value = 0;
                    FileProgressBar.Maximum = patienten.Count;
                    MainStatusBar.Visibility = Visibility.Visible;

                    await Task.Run(() => 
                    {
                        _controller.ClearAllAfspraken();
                        _controller.ClearAllPersonen();

                        foreach (PatientPersoonDto Patient in patienten)
                        {
                            try
                            {
                                _controller.InsertPersoon(Patient);
                            }
                            catch
                            {
                                Debug.WriteLine($"Fout in Persoon");
                            }
                            finally
                            {
                                Application.Current.Dispatcher.Invoke(() => FileProgressBar.Value++);
                            }
                        }
                    });

                    MainStatusBar.Visibility = Visibility.Collapsed;
                }

                if (BestandenSelectedListView.Items.Contains("Afspraken.csv"))
                {
                    var afspraken = _controller.GetAllAfspraken();

                    FileNameTextBox.Text = "Afspraken.csv";

                    FileProgressBar.Value = 0;
                    FileProgressBar.Maximum = afspraken.Count;
                    MainStatusBar.Visibility = Visibility.Visible;

                    await Task.Run(() => 
                    {
                        _controller.ClearAllAfspraken();

                        foreach (AfspraakDto afspraak in afspraken)
                        {
                            try
                            {
                                _controller.InsertAfspraak(afspraak);
                            }
                            catch
                            {
                                Debug.WriteLine($"Fout in Afspraak");
                            }
                            finally
                            {
                                Application.Current.Dispatcher.Invoke(() => FileProgressBar.Value++);
                            }
                        }
                    });

                    MainStatusBar.Visibility = Visibility.Collapsed;
                }


                MessageBox.Show("All files parsed");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}