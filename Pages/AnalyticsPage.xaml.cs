using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;

namespace TrainingControlPanelDashboard.Pages
{
    public partial class AnalyticsPage : ContentPage
    {
        private readonly IDataService _dataService;
        private ObservableCollection<ModelPerformance> _topModels;

        public AnalyticsPage()
        {
            InitializeComponent();
            _dataService = new DataService();
            _topModels = new ObservableCollection<ModelPerformance>();
            LoadAnalytics();
        }

        private async void LoadAnalytics()
        {
            try
            {
                // Load data
                var models = await _dataService.GetModelsAsync();
                var sessions = await _dataService.GetTrainingSessionsAsync();
                var programs = await _dataService.GetTrainingProgramsAsync();

                // Calculate analytics
                CalculateMetrics(models, sessions, programs);
                LoadTopPerformers(models, sessions);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load analytics: {ex.Message}", "OK");
            }
        }

        private void CalculateMetrics(IEnumerable<Model> models, IEnumerable<TrainingSession> sessions, IEnumerable<TrainingProgram> programs)
        {
            // Model completion rate (mock: percent of models with at least one completed session)
            var completedModelIds = sessions.Where(s => s.Status == "Completed").Select(s => s.ModelType).Distinct().Count();
            var totalModels = models.Count();
            var completionRate = totalModels > 0 ? (double)completedModelIds / totalModels : 0;

            CompletionRateLabel.Text = $"{completionRate * 100:F0}%";
            CompletionRateProgress.Progress = completionRate;

            // Average training duration
            var avgDuration = sessions.Any() && sessions.All(s => s.Duration.HasValue) ? sessions.Average(s => s.Duration.Value.TotalMinutes) : 0;
            var hours = (int)(avgDuration / 60);
            var minutes = (int)(avgDuration % 60);
            AvgDurationLabel.Text = $"{hours}h {minutes}m";

            // Active models
            var activeModels = models.Count(m => m.Status == "Active" || m.Status == "Training");
            ActiveModelsLabel.Text = activeModels.ToString();

            // Running programs
            var runningPrograms = programs.Count(p => p.Status == "Running");
            RunningProgramsLabel.Text = runningPrograms.ToString();
        }

        private void LoadTopPerformers(IEnumerable<Model> models, IEnumerable<TrainingSession> sessions)
        {
            var modelPerformances = new List<ModelPerformance>();
            var random = new Random();

            int rank = 1;
            foreach (var model in models.Take(5))
            {
                // Calculate mock performance score (in a real app, this would be based on actual metrics)
                var modelSessions = sessions.Where(s => s.ModelType == model.Type);
                var avgAccuracy = modelSessions.Any() ? modelSessions.Average(s => s.Accuracy) : random.NextDouble() * 0.2 + 0.8;

                modelPerformances.Add(new ModelPerformance
                {
                    Rank = rank++,
                    Name = model.Name,
                    Type = model.Type,
                    PerformanceScore = avgAccuracy * 100
                });
            }

            // Sort by performance score
            modelPerformances = modelPerformances.OrderByDescending(mp => mp.PerformanceScore).ToList();

            // Update ranks
            for (int i = 0; i < modelPerformances.Count; i++)
            {
                modelPerformances[i].Rank = i + 1;
            }

            _topModels.Clear();
            foreach (var performance in modelPerformances)
            {
                _topModels.Add(performance);
            }

            TopModelsCollectionView.ItemsSource = _topModels;
        }
    }

    public class ModelPerformance
    {
        public int Rank { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public double PerformanceScore { get; set; }
    }
}
