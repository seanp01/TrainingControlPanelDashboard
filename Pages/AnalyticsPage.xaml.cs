using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;

namespace TrainingControlPanelDashboard.Pages
{
    public partial class AnalyticsPage : ContentPage
    {
        private readonly IDataService _dataService;
        private ObservableCollection<AthletePerformance> _topAthletes;

        public AnalyticsPage()
        {
            InitializeComponent();
            _dataService = new DataService();
            _topAthletes = new ObservableCollection<AthletePerformance>();
            
            LoadAnalytics();
        }

        private async void LoadAnalytics()
        {
            try
            {
                // Load data
                var athletes = await _dataService.GetAthletesAsync();
                var sessions = await _dataService.GetTrainingSessionsAsync();
                var programs = await _dataService.GetTrainingProgramsAsync();

                // Calculate analytics
                CalculateMetrics(athletes, sessions, programs);
                LoadTopPerformers(athletes, sessions);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load analytics: {ex.Message}", "OK");
            }
        }

        private void CalculateMetrics(IEnumerable<Athlete> athletes, IEnumerable<TrainingSession> sessions, IEnumerable<TrainingProgram> programs)
        {
            // Session completion rate
            var completedSessions = sessions.Count(s => s.Status == "Completed");
            var totalSessions = sessions.Count();
            var completionRate = totalSessions > 0 ? (double)completedSessions / totalSessions : 0;
            
            CompletionRateLabel.Text = $"{completionRate * 100:F0}%";
            CompletionRateProgress.Progress = completionRate;

            // Average session duration
            var avgDuration = sessions.Any() ? sessions.Average(s => s.Duration.TotalMinutes) : 0;
            var hours = (int)(avgDuration / 60);
            var minutes = (int)(avgDuration % 60);
            AvgDurationLabel.Text = $"{hours}h {minutes}m";

            // Active athletes
            var activeAthletes = athletes.Count(a => a.Status == "Active");
            ActiveAthletesLabel.Text = activeAthletes.ToString();

            // Running programs
            var runningPrograms = programs.Count(p => p.StartDate <= DateTime.Today && p.EndDate >= DateTime.Today);
            RunningProgramsLabel.Text = runningPrograms.ToString();
        }

        private void LoadTopPerformers(IEnumerable<Athlete> athletes, IEnumerable<TrainingSession> sessions)
        {
            var athletePerformances = new List<AthletePerformance>();
            var random = new Random();

            int rank = 1;
            foreach (var athlete in athletes.Take(5))
            {
                // Calculate mock performance score (in a real app, this would be based on actual metrics)
                var athleteSessions = sessions.Where(s => s.AthleteId == athlete.Id);
                var completedCount = athleteSessions.Count(s => s.Status == "Completed");
                var totalCount = athleteSessions.Count();
                var performanceScore = totalCount > 0 ? (double)completedCount / totalCount * 100 : random.NextDouble() * 30 + 70;

                athletePerformances.Add(new AthletePerformance
                {
                    Rank = rank++,
                    Name = athlete.Name,
                    Sport = athlete.Sport,
                    PerformanceScore = performanceScore
                });
            }

            // Sort by performance score
            athletePerformances = athletePerformances.OrderByDescending(ap => ap.PerformanceScore).ToList();
            
            // Update ranks
            for (int i = 0; i < athletePerformances.Count; i++)
            {
                athletePerformances[i].Rank = i + 1;
            }

            _topAthletes.Clear();
            foreach (var performance in athletePerformances)
            {
                _topAthletes.Add(performance);
            }

            TopAthletesCollectionView.ItemsSource = _topAthletes;
        }
    }

    public class AthletePerformance
    {
        public int Rank { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sport { get; set; } = string.Empty;
        public double PerformanceScore { get; set; }
    }
}
