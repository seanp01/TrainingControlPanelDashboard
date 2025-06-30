using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TrainingControlPanelDashboard.Pages
{
    public partial class TrainingSessionsPage : ContentPage
    {
        private readonly IDataService _dataService;
        private ObservableCollection<TrainingSessionViewModel> _sessions;
        private ObservableCollection<TrainingSessionViewModel> _filteredSessions;
        private string _searchText = string.Empty;
        private string _statusFilter = "All";

        public TrainingSessionsPage()
        {
            InitializeComponent();
            _dataService = new DataService();
            _sessions = new ObservableCollection<TrainingSessionViewModel>();
            _filteredSessions = new ObservableCollection<TrainingSessionViewModel>();
            
            StatusFilterPicker.SelectedIndex = 0; // Select "All"
            
            LoadSessions();
        }

        private async void LoadSessions()
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                var sessions = await _dataService.GetTrainingSessionsAsync();
                
                _sessions.Clear();
                foreach (var session in sessions.OrderBy(s => s.ScheduledDateTime))
                {
                    _sessions.Add(new TrainingSessionViewModel(session, _dataService));
                }

                ApplyFilters();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load sessions: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }

        private void ApplyFilters()
        {
            var filtered = _sessions.AsEnumerable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                filtered = filtered.Where(s => 
                    s.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                    s.Type.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                    s.Location.Contains(_searchText, StringComparison.OrdinalIgnoreCase));
            }

            // Apply status filter
            if (_statusFilter != "All")
            {
                filtered = filtered.Where(s => s.Status == _statusFilter);
            }

            _filteredSessions.Clear();
            foreach (var session in filtered)
            {
                _filteredSessions.Add(session);
            }

            SessionsCollectionView.ItemsSource = _filteredSessions;
        }

        private async void OnAddSessionClicked(object sender, EventArgs e)
        {
            // Navigate to add session page (to be implemented)
            await DisplayAlert("Add Session", "Add new session functionality will be implemented", "OK");
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = e.NewTextValue ?? string.Empty;
            ApplyFilters();
        }

        private void OnStatusFilterChanged(object sender, EventArgs e)
        {
            if (StatusFilterPicker.SelectedItem != null)
            {
                _statusFilter = StatusFilterPicker.SelectedItem.ToString() ?? "All";
                ApplyFilters();
            }
        }
    }

    public class TrainingSessionViewModel : TrainingSession
    {
        private readonly IDataService _dataService;

        public TrainingSessionViewModel(TrainingSession session, IDataService dataService)
        {
            _dataService = dataService;
            
            // Copy properties from the source session
            Id = session.Id;
            Name = session.Name;
            ScheduledDateTime = session.ScheduledDateTime;
            Duration = session.Duration;
            Type = session.Type;
            Description = session.Description;
            Status = session.Status;
            Location = session.Location;
            AthleteId = session.AthleteId;
            ProgramId = session.ProgramId;
        }

        public string StatusColor
        {
            get
            {
                return Status switch
                {
                    "Scheduled" => "#2196F3",
                    "In Progress" => "#FF9800",
                    "Completed" => "#4CAF50",
                    "Cancelled" => "#F44336",
                    _ => "#9E9E9E"
                };
            }
        }

        public ICommand EditCommand => new Command<TrainingSessionViewModel>(async (session) =>
        {
            await Application.Current?.MainPage?.DisplayAlert("Edit", $"Edit session: {session?.Name}", "OK");
        });

        public ICommand DeleteCommand => new Command<TrainingSessionViewModel>(async (session) =>
        {
            if (session != null)
            {
                bool confirm = await Application.Current?.MainPage?.DisplayAlert(
                    "Confirm Delete", 
                    $"Are you sure you want to delete '{session.Name}'?", 
                    "Yes", "No");

                if (confirm)
                {
                    await _dataService.DeleteTrainingSessionAsync(session.Id);
                    // Refresh the page would be implemented here
                }
            }
        });
    }
}
