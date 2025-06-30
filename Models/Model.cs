using System.ComponentModel;

namespace TrainingControlPanelDashboard.Models
{
    public class Model : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _type = string.Empty;
        private string _dataset = string.Empty;
        private string _status = "Idle";
        private double _accuracy;
        private double _loss;
        private DateTime? _lastRun;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
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

        public DateTime? LastRun
        {
            get => _lastRun;
            set { _lastRun = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
