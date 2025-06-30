using System.ComponentModel;

namespace TrainingControlPanelDashboard.Models
{
    public class TrainingProgram : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _objective = string.Empty;
        private string _dataset = string.Empty;
        private string _status = "Draft";
        private DateTime _created;
        private DateTime? _lastRun;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _level = string.Empty;
        private string _category = string.Empty;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Objective
        {
            get => _objective;
            set { _objective = value; OnPropertyChanged(); }
        }

        public string Dataset
        {
            get => _dataset;
            set { _dataset = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public DateTime Created
        {
            get => _created;
            set { _created = value; OnPropertyChanged(); }
        }

        public DateTime? LastRun
        {
            get => _lastRun;
            set { _lastRun = value; OnPropertyChanged(); }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); }
        }

        public string Level
        {
            get => _level;
            set { _level = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
