using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;

namespace TrainingControlPanelDashboard.Services
{
    public interface IDataService
    {
        Task<ObservableCollection<Athlete>> GetAthletesAsync();
        Task<ObservableCollection<TrainingSession>> GetTrainingSessionsAsync();
        Task<ObservableCollection<TrainingProgram>> GetTrainingProgramsAsync();
        Task<ObservableCollection<Exercise>> GetExercisesAsync();
        Task<Athlete> AddAthleteAsync(Athlete athlete);
        Task<TrainingSession> AddTrainingSessionAsync(TrainingSession session);
        Task<TrainingProgram> AddTrainingProgramAsync(TrainingProgram program);
        Task<bool> UpdateAthleteAsync(Athlete athlete);
        Task<bool> UpdateTrainingSessionAsync(TrainingSession session);
        Task<bool> DeleteAthleteAsync(int athleteId);
        Task<bool> DeleteTrainingSessionAsync(int sessionId);
    }

    public class DataService : IDataService
    {
        private readonly ObservableCollection<Athlete> _athletes;
        private readonly ObservableCollection<TrainingSession> _trainingSessions;
        private readonly ObservableCollection<TrainingProgram> _trainingPrograms;
        private readonly ObservableCollection<Exercise> _exercises;
        private int _nextAthleteId = 1;
        private int _nextSessionId = 1;
        private int _nextProgramId = 1;
        private int _nextExerciseId = 1;

        public DataService()
        {
            _athletes = new ObservableCollection<Athlete>();
            _trainingSessions = new ObservableCollection<TrainingSession>();
            _trainingPrograms = new ObservableCollection<TrainingProgram>();
            _exercises = new ObservableCollection<Exercise>();
            
            // Initialize with sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Sample Athletes
            var athletes = new[]
            {
                new Athlete { Id = _nextAthleteId++, Name = "John Smith", Email = "john.smith@email.com", DateOfBirth = new DateTime(1995, 5, 15), Sport = "Football", Position = "Quarterback", Height = 185, Weight = 82 },
                new Athlete { Id = _nextAthleteId++, Name = "Sarah Johnson", Email = "sarah.johnson@email.com", DateOfBirth = new DateTime(1998, 8, 22), Sport = "Basketball", Position = "Point Guard", Height = 170, Weight = 65 },
                new Athlete { Id = _nextAthleteId++, Name = "Mike Wilson", Email = "mike.wilson@email.com", DateOfBirth = new DateTime(1996, 12, 3), Sport = "Soccer", Position = "Midfielder", Height = 178, Weight = 75 },
                new Athlete { Id = _nextAthleteId++, Name = "Emma Davis", Email = "emma.davis@email.com", DateOfBirth = new DateTime(1997, 3, 18), Sport = "Tennis", Position = "Singles", Height = 165, Weight = 60 }
            };

            foreach (var athlete in athletes)
            {
                _athletes.Add(athlete);
            }

            // Sample Exercises
            var exercises = new[]
            {
                new Exercise { Id = _nextExerciseId++, Name = "Push-ups", Category = "Strength", MuscleGroup = "Chest", Equipment = "None", Sets = 3, Reps = 15, Weight = 0, RestTime = TimeSpan.FromMinutes(1) },
                new Exercise { Id = _nextExerciseId++, Name = "Squats", Category = "Strength", MuscleGroup = "Legs", Equipment = "None", Sets = 3, Reps = 12, Weight = 0, RestTime = TimeSpan.FromMinutes(1.5) },
                new Exercise { Id = _nextExerciseId++, Name = "Bench Press", Category = "Strength", MuscleGroup = "Chest", Equipment = "Barbell", Sets = 3, Reps = 10, Weight = 80, RestTime = TimeSpan.FromMinutes(2) },
                new Exercise { Id = _nextExerciseId++, Name = "Deadlift", Category = "Strength", MuscleGroup = "Back", Equipment = "Barbell", Sets = 3, Reps = 8, Weight = 100, RestTime = TimeSpan.FromMinutes(3) }
            };

            foreach (var exercise in exercises)
            {
                _exercises.Add(exercise);
            }

            // Sample Training Programs
            var programs = new[]
            {
                new TrainingProgram { Id = _nextProgramId++, Name = "Beginner Strength", Description = "Basic strength training for beginners", StartDate = DateTime.Today.AddDays(-30), EndDate = DateTime.Today.AddDays(60), Level = "Beginner", Category = "Strength" },
                new TrainingProgram { Id = _nextProgramId++, Name = "Advanced Cardio", Description = "High-intensity cardiovascular training", StartDate = DateTime.Today.AddDays(-15), EndDate = DateTime.Today.AddDays(45), Level = "Advanced", Category = "Cardio" },
                new TrainingProgram { Id = _nextProgramId++, Name = "Sport-Specific Training", Description = "Training tailored for specific sports", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(90), Level = "Intermediate", Category = "Sport-Specific" }
            };

            foreach (var program in programs)
            {
                _trainingPrograms.Add(program);
            }

            // Sample Training Sessions
            var sessions = new[]
            {
                new TrainingSession { Id = _nextSessionId++, Name = "Morning Strength", ScheduledDateTime = DateTime.Today.AddHours(8), Duration = TimeSpan.FromHours(1.5), Type = "Strength", Description = "Full body strength training", Status = "Completed", Location = "Gym A", AthleteId = 1, ProgramId = 1 },
                new TrainingSession { Id = _nextSessionId++, Name = "Cardio Blast", ScheduledDateTime = DateTime.Today.AddDays(1).AddHours(6), Duration = TimeSpan.FromHours(1), Type = "Cardio", Description = "High-intensity interval training", Status = "Scheduled", Location = "Track", AthleteId = 2, ProgramId = 2 },
                new TrainingSession { Id = _nextSessionId++, Name = "Skills Training", ScheduledDateTime = DateTime.Today.AddDays(2).AddHours(16), Duration = TimeSpan.FromHours(2), Type = "Skills", Description = "Sport-specific skill development", Status = "Scheduled", Location = "Field", AthleteId = 3, ProgramId = 3 }
            };

            foreach (var session in sessions)
            {
                _trainingSessions.Add(session);
            }
        }

        public async Task<ObservableCollection<Athlete>> GetAthletesAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _athletes;
        }

        public async Task<ObservableCollection<TrainingSession>> GetTrainingSessionsAsync()
        {
            await Task.Delay(100);
            return _trainingSessions;
        }

        public async Task<ObservableCollection<TrainingProgram>> GetTrainingProgramsAsync()
        {
            await Task.Delay(100);
            return _trainingPrograms;
        }

        public async Task<ObservableCollection<Exercise>> GetExercisesAsync()
        {
            await Task.Delay(100);
            return _exercises;
        }

        public async Task<Athlete> AddAthleteAsync(Athlete athlete)
        {
            await Task.Delay(50);
            athlete.Id = _nextAthleteId++;
            _athletes.Add(athlete);
            return athlete;
        }

        public async Task<TrainingSession> AddTrainingSessionAsync(TrainingSession session)
        {
            await Task.Delay(50);
            session.Id = _nextSessionId++;
            _trainingSessions.Add(session);
            return session;
        }

        public async Task<TrainingProgram> AddTrainingProgramAsync(TrainingProgram program)
        {
            await Task.Delay(50);
            program.Id = _nextProgramId++;
            _trainingPrograms.Add(program);
            return program;
        }

        public async Task<bool> UpdateAthleteAsync(Athlete athlete)
        {
            await Task.Delay(50);
            var existing = _athletes.FirstOrDefault(a => a.Id == athlete.Id);
            if (existing != null)
            {
                var index = _athletes.IndexOf(existing);
                _athletes[index] = athlete;
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTrainingSessionAsync(TrainingSession session)
        {
            await Task.Delay(50);
            var existing = _trainingSessions.FirstOrDefault(s => s.Id == session.Id);
            if (existing != null)
            {
                var index = _trainingSessions.IndexOf(existing);
                _trainingSessions[index] = session;
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAthleteAsync(int athleteId)
        {
            await Task.Delay(50);
            var athlete = _athletes.FirstOrDefault(a => a.Id == athleteId);
            if (athlete != null)
            {
                _athletes.Remove(athlete);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTrainingSessionAsync(int sessionId)
        {
            await Task.Delay(50);
            var session = _trainingSessions.FirstOrDefault(s => s.Id == sessionId);
            if (session != null)
            {
                _trainingSessions.Remove(session);
                return true;
            }
            return false;
        }
    }
}
