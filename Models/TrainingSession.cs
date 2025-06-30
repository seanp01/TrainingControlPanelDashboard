using System.ComponentModel;

namespace TrainingControlPanelDashboard.Models
{
    public class TrainingSession : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private DateTime _scheduledDateTime;
        private TimeSpan _duration;
        private string _type = string.Empty;
        private string _description = string.Empty;
        private string _status = "Scheduled";
        private string _location = string.Empty;

        public int Id { get; set; }
        
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTime ScheduledDateTime
        {
            get => _scheduledDateTime;
            set
            {
                _scheduledDateTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public int? AthleteId { get; set; }
        public Athlete? Athlete { get; set; }
        
        public int? ProgramId { get; set; }
        public TrainingProgram? Program { get; set; }

        public List<Exercise> Exercises { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
