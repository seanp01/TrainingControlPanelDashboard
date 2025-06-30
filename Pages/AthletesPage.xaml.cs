using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TrainingControlPanelDashboard.Pages
{
    public partial class AthletesPage : ContentPage
    {
        private readonly IDataService _dataService;
        private ObservableCollection<AthleteViewModel> _athletes;
        private ObservableCollection<AthleteViewModel> _filteredAthletes;
        private string _searchText = string.Empty;
        private string _sportFilter = "All Sports";

        public AthletesPage()
        {
            InitializeComponent();
            _dataService = new DataService();
            _athletes = new ObservableCollection<AthleteViewModel>();
            _filteredAthletes = new ObservableCollection<AthleteViewModel>();
            
            SportFilterPicker.SelectedIndex = 0; // Select "All Sports"
            
            LoadAthletes();
        }

        private async void LoadAthletes()
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                var athletes = await _dataService.GetAthletesAsync();
                
                _athletes.Clear();
                foreach (var athlete in athletes.OrderBy(a => a.Name))
                {
                    _athletes.Add(new AthleteViewModel(athlete, _dataService));
                }

                ApplyFilters();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load athletes: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }

        private void ApplyFilters()
        {
            var filtered = _athletes.AsEnumerable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                filtered = filtered.Where(a => 
                    a.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                    a.Sport.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                    a.Position.Contains(_searchText, StringComparison.OrdinalIgnoreCase));
            }

            // Apply sport filter
            if (_sportFilter != "All Sports")
            {
                filtered = filtered.Where(a => a.Sport == _sportFilter);
            }

            _filteredAthletes.Clear();
            foreach (var athlete in filtered)
            {
                _filteredAthletes.Add(athlete);
            }

            AthletesCollectionView.ItemsSource = _filteredAthletes;
        }

        private async void OnAddAthleteClicked(object sender, EventArgs e)
        {
            // Navigate to add athlete page (to be implemented)
            await DisplayAlert("Add Athlete", "Add new athlete functionality will be implemented", "OK");
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = e.NewTextValue ?? string.Empty;
            ApplyFilters();
        }

        private void OnSportFilterChanged(object sender, EventArgs e)
        {
            if (SportFilterPicker.SelectedItem != null)
            {
                _sportFilter = SportFilterPicker.SelectedItem.ToString() ?? "All Sports";
                ApplyFilters();
            }
        }
    }

    public class AthleteViewModel : Athlete
    {
        private readonly IDataService _dataService;

        public AthleteViewModel(Athlete athlete, IDataService dataService)
        {
            _dataService = dataService;
            
            // Copy properties from the source athlete
            Id = athlete.Id;
            Name = athlete.Name;
            Email = athlete.Email;
            DateOfBirth = athlete.DateOfBirth;
            Sport = athlete.Sport;
            Position = athlete.Position;
            Height = athlete.Height;
            Weight = athlete.Weight;
            Status = athlete.Status;
        }

        public string Initials
        {
            get
            {
                var parts = Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                    return $"{parts[0][0]}{parts[1][0]}".ToUpper();
                if (parts.Length == 1)
                    return parts[0][0].ToString().ToUpper();
                return "??";
            }
        }

        public ICommand EditCommand => new Command(async () =>
        {
            await Application.Current?.MainPage?.DisplayAlert("Edit", $"Edit athlete: {Name}", "OK");
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            bool confirm = await Application.Current?.MainPage?.DisplayAlert(
                "Confirm Delete", 
                $"Are you sure you want to delete '{Name}'?", 
                "Yes", "No");
            
            if (confirm)
            {
                await _dataService.DeleteAthleteAsync(Id);
                // Refresh the page would be implemented here
            }
        });
    }
}
