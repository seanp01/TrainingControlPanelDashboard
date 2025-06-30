using TrainingControlPanelDashboard.Services;
using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TrainingControlPanelDashboard.Pages
{
    public partial class ModelsPage : ContentPage
    {
        private readonly IDataService _dataService;
        private ObservableCollection<ModelViewModel> _models;
        private ObservableCollection<ModelViewModel> _filteredModels;
        private string _searchText = string.Empty;
        private string _typeFilter = "All Types";

        public ModelsPage()
        {
            InitializeComponent();
            _dataService = new DataService();
            _models = new ObservableCollection<ModelViewModel>();
            _filteredModels = new ObservableCollection<ModelViewModel>();
            TypeFilterPicker.SelectedIndex = 0; // Select "All Types"
            LoadModels();
        }

        private async void LoadModels()
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                var models = await _dataService.GetModelsAsync();

                _models.Clear();
                foreach (var model in models.OrderBy(m => m.Name))
                {
                    _models.Add(new ModelViewModel(model, _dataService));
                }

                ApplyFilters();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load models: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }

        private void ApplyFilters()
        {
            var filtered = _models.AsEnumerable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                filtered = filtered.Where(m =>
                    m.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                    m.Type.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                    m.Dataset.Contains(_searchText, StringComparison.OrdinalIgnoreCase));
            }

            // Apply type filter
            if (_typeFilter != "All Types")
            {
                filtered = filtered.Where(m => m.Type == _typeFilter);
            }

            _filteredModels.Clear();
            foreach (var model in filtered)
            {
                _filteredModels.Add(model);
            }

            ModelsCollectionView.ItemsSource = _filteredModels;
        }

        private async void OnAddModelClicked(object sender, EventArgs e)
        {
            // Navigate to add model page (to be implemented)
            await DisplayAlert("Add Model", "Add new model functionality will be implemented", "OK");
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = e.NewTextValue ?? string.Empty;
            ApplyFilters();
        }

        private void OnTypeFilterChanged(object sender, EventArgs e)
        {
            if (TypeFilterPicker.SelectedItem != null)
            {
                _typeFilter = TypeFilterPicker.SelectedItem.ToString() ?? "All Types";
                ApplyFilters();
            }
        }
    }

    public class ModelViewModel : Model
    {
        private readonly IDataService _dataService;

        public ModelViewModel(Model model, IDataService dataService)
        {
            _dataService = dataService;
            // Copy properties from the source model
            Id = model.Id;
            Name = model.Name;
            Type = model.Type;
            Dataset = model.Dataset;
            Status = model.Status;
            Accuracy = model.Accuracy;
            Loss = model.Loss;
            LastRun = model.LastRun;
        }

        public string TypeShort => !string.IsNullOrEmpty(Type) ? Type.Split(' ')[0][..1].ToUpper() : "M";

        public ICommand EditCommand => new Command(async () =>
        {
            await Application.Current?.MainPage?.DisplayAlert("Edit", $"Edit model: {Name}", "OK");
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            bool confirm = await Application.Current?.MainPage?.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete '{Name}'?",
                "Yes", "No");
            if (confirm)
            {
                await _dataService.DeleteModelAsync(Id);
                // Refresh the page would be implemented here
            }
        });

        public ICommand StartTrainingCommand => new Command(async () =>
        {
            await Application.Current?.MainPage?.DisplayAlert("Start Training", $"Training started for model: {Name}", "OK");
            // Actual training logic would be implemented here
        });
    }
}
