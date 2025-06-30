using System.ComponentModel;

namespace TrainingControlPanelDashboard.Models
{
    public class TrainingSession : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _status = "Pending";
        private DateTime _scheduled;
        private DateTime? _started;
        private DateTime? _ended;
        private TimeSpan? _duration;
        private string _modelType = string.Empty;
        private string _dataset = string.Empty;
        private double _accuracy;
        private double _loss;

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

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public DateTime Scheduled
        {
            get => _scheduled;
            set { _scheduled = value; OnPropertyChanged(); }
        }

        public DateTime? Started
        {
            get => _started;
            set { _started = value; OnPropertyChanged(); }
        }

        public DateTime? Ended
        {
            get => _ended;
            set { _ended = value; OnPropertyChanged(); }
        }

        public TimeSpan? Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged(); }
        }

        public string ModelType
        {
            get => _modelType;
            set { _modelType = value; OnPropertyChanged(); }
        }

        public string Dataset
        {
            get => _dataset;
            set { _dataset = value; OnPropertyChanged(); }
        }

        public double Accuracy
        {
            get => _accuracy;
            set { _accuracy = value; OnPropertyChanged(); }
        }

        public double Loss
        {
            get => _loss;
            set { _loss = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
