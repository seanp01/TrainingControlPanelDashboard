using System.ComponentModel;

namespace TrainingControlPanelDashboard.Models
{
    public class Exercise : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _category = string.Empty;
        private string _muscleGroup = string.Empty;
        private string _equipment = string.Empty;
        private int _sets;
        private int _reps;
        private double _weight;
        private TimeSpan _restTime;

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

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        public string MuscleGroup
        {
            get => _muscleGroup;
            set
            {
                _muscleGroup = value;
                OnPropertyChanged();
            }
        }

        public string Equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }

        public int Sets
        {
            get => _sets;
            set
            {
                _sets = value;
                OnPropertyChanged();
            }
        }

        public int Reps
        {
            get => _reps;
            set
            {
                _reps = value;
                OnPropertyChanged();
            }
        }

        public double Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan RestTime
        {
            get => _restTime;
            set
            {
                _restTime = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
