using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;

namespace TrainingControlPanelDashboard
{
    public partial class MainPage : ContentPage
    {
        private readonly IDataService _dataService;
        private ObservableCollection<TrainingSession> _upcomingSessions;
        private ObservableCollection<TrainingSession> _recentActivity;

        public MainPage()
        {
            InitializeComponent();
            _dataService = new DataService();
            _upcomingSessions = new ObservableCollection<TrainingSession>();
            _recentActivity = new ObservableCollection<TrainingSession>();
            
            LoadDashboardData();
        }

        private async void LoadDashboardData()
        {
            try
            {
                // Load statistics
                var athletes = await _dataService.GetAthletesAsync();
                var sessions = await _dataService.GetTrainingSessionsAsync();
                var programs = await _dataService.GetTrainingProgramsAsync();

                // Update statistics
                TotalAthletesLabel.Text = athletes.Count.ToString();
                
                var todaySessions = sessions.Where(s => s.ScheduledDateTime.Date == DateTime.Today).Count();
                TotalSessionsLabel.Text = todaySessions.ToString();
                
                var activePrograms = programs.Where(p => p.EndDate >= DateTime.Today).Count();
                ActiveProgramsLabel.Text = activePrograms.ToString();
                
                var completedSessions = sessions.Where(s => s.Status == "Completed").Count();
                var completionRate = sessions.Count > 0 ? (double)completedSessions / sessions.Count * 100 : 0;
                CompletionRateLabel.Text = $"{completionRate:F0}%";

                // Load upcoming sessions
                var upcoming = sessions
                    .Where(s => s.ScheduledDateTime >= DateTime.Now && s.Status == "Scheduled")
                    .OrderBy(s => s.ScheduledDateTime)
                    .Take(5);

                _upcomingSessions.Clear();
                foreach (var session in upcoming)
                {
                    _upcomingSessions.Add(session);
                }
                UpcomingSessionsCollectionView.ItemsSource = _upcomingSessions;

                // Load recent activity (recent sessions)
                var recent = sessions
                    .Where(s => s.ScheduledDateTime >= DateTime.Today.AddDays(-7))
                    .OrderByDescending(s => s.ScheduledDateTime)
                    .Take(5);

                _recentActivity.Clear();
                foreach (var session in recent)
                {
                    _recentActivity.Add(session);
                }
                RecentActivityCollectionView.ItemsSource = _recentActivity;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load dashboard data: {ex.Message}", "OK");
            }
        }

        private async void OnNewSessionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//sessions");
        }

        private async void OnViewAthletesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//athletes");
        }
    }
}
