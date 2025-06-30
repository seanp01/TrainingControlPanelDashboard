using System.ComponentModel;

namespace TrainingControlPanelDashboard.Models
{
    public class Exercise : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _rewardType = string.Empty;
        private double _rewardValue;
        private string _action = string.Empty;
        private string _observation = string.Empty;
        private string _status = "Pending";
        private DateTime? _executed;

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

        public string RewardType
        {
            get => _rewardType;
            set { _rewardType = value; OnPropertyChanged(); }
        }

        public double RewardValue
        {
            get => _rewardValue;
            set { _rewardValue = value; OnPropertyChanged(); }
        }

        public string Action
        {
            get => _action;
            set { _action = value; OnPropertyChanged(); }
        }

        public string Observation
        {
            get => _observation;
            set { _observation = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public DateTime? Executed
        {
            get => _executed;
            set { _executed = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
