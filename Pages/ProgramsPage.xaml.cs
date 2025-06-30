using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TrainingControlPanelDashboard.Pages
{
    public partial class ProgramsPage : ContentPage
    {
        private readonly IDataService _dataService;
        private ObservableCollection<ProgramViewModel> _programs;
        private ObservableCollection<ProgramViewModel> _filteredPrograms;
        private string _searchText = string.Empty;
        private string _categoryFilter = "All";
        private string _levelFilter = "All";

        public ProgramsPage()
        {
            InitializeComponent();
            _dataService = new DataService();
            _programs = new ObservableCollection<ProgramViewModel>();
            _filteredPrograms = new ObservableCollection<ProgramViewModel>();
            
            CategoryFilterPicker.SelectedIndex = 0; // Select "All"
            LevelFilterPicker.SelectedIndex = 0; // Select "All"
            
            LoadPrograms();
        }

        private async void LoadPrograms()
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                var programs = await _dataService.GetTrainingProgramsAsync();
                
                _programs.Clear();
                foreach (var program in programs.OrderBy(p => p.StartDate))
                {
                    _programs.Add(new ProgramViewModel(program));
                }

                ApplyFilters();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load programs: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }

        private void ApplyFilters()
        {
            var filtered = _programs.AsEnumerable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                filtered = filtered.Where(p => 
                    p.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase));
            }

            // Apply category filter
            if (_categoryFilter != "All")
            {
                filtered = filtered.Where(p => p.Category == _categoryFilter);
            }

            // Apply level filter
            if (_levelFilter != "All")
            {
                filtered = filtered.Where(p => p.Level == _levelFilter);
            }

            _filteredPrograms.Clear();
            foreach (var program in filtered)
            {
                _filteredPrograms.Add(program);
            }

            ProgramsCollectionView.ItemsSource = _filteredPrograms;
        }

        private async void OnAddProgramClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Add Program", "Add new program functionality will be implemented", "OK");
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = e.NewTextValue ?? string.Empty;
            ApplyFilters();
        }

        private void OnCategoryFilterChanged(object sender, EventArgs e)
        {
            if (CategoryFilterPicker.SelectedItem != null)
            {
                _categoryFilter = CategoryFilterPicker.SelectedItem.ToString() ?? "All";
                ApplyFilters();
            }
        }

        private void OnLevelFilterChanged(object sender, EventArgs e)
        {
            if (LevelFilterPicker.SelectedItem != null)
            {
                _levelFilter = LevelFilterPicker.SelectedItem.ToString() ?? "All";
                ApplyFilters();
            }
        }
    }

    public class ProgramViewModel : TrainingProgram
    {
        public ProgramViewModel(TrainingProgram program)
        {
            // Copy properties from the source program
            Id = program.Id;
            Name = program.Name;
            Description = program.Description;
            StartDate = program.StartDate;
            EndDate = program.EndDate;
            Level = program.Level;
            Category = program.Category;
        }

        public double Progress
        {
            get
            {
                var totalDays = (EndDate - StartDate).TotalDays;
                if (totalDays <= 0) return 0;

                var completedDays = (DateTime.Today - StartDate).TotalDays;
                if (completedDays < 0) return 0;
                if (completedDays > totalDays) return 1;

                return completedDays / totalDays;
            }
        }

        public string ProgressText
        {
            get
            {
                var percentage = Progress * 100;
                return $"Progress: {percentage:F0}%";
            }
        }

        public ICommand EditCommand => new Command(async () =>
        {
            await Application.Current?.MainPage?.DisplayAlert("Edit", $"Edit program: {Name}", "OK");
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            bool confirm = await Application.Current?.MainPage?.DisplayAlert(
                "Confirm Delete", 
                $"Are you sure you want to delete '{Name}'?", 
                "Yes", "No");
            
            if (confirm)
            {
                // Delete logic would be implemented here
                await Application.Current?.MainPage?.DisplayAlert("Deleted", $"Program '{Name}' has been deleted.", "OK");
            }
        });
    }
}
