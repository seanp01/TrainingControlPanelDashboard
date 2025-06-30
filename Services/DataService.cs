using TrainingControlPanelDashboard.Models;
using System.Collections.ObjectModel;

namespace TrainingControlPanelDashboard.Services
{
    public interface IDataService
    {
        Task<ObservableCollection<Model>> GetModelsAsync();
        Task<Model> AddModelAsync(Model model);
        Task<bool> UpdateModelAsync(Model model);
        Task<bool> DeleteModelAsync(int modelId);

        Task<ObservableCollection<TrainingSession>> GetTrainingSessionsAsync();
        Task<TrainingSession> AddTrainingSessionAsync(TrainingSession session);
        Task<bool> UpdateTrainingSessionAsync(TrainingSession session);
        Task<bool> DeleteTrainingSessionAsync(int sessionId);

        Task<ObservableCollection<TrainingProgram>> GetTrainingProgramsAsync();
        Task<TrainingProgram> AddTrainingProgramAsync(TrainingProgram program);
        Task<bool> UpdateTrainingProgramAsync(TrainingProgram program);
        Task<bool> DeleteTrainingProgramAsync(int programId);

        Task<ObservableCollection<Exercise>> GetExercisesAsync();
        Task<Exercise> AddExerciseAsync(Exercise exercise);
        Task<bool> UpdateExerciseAsync(Exercise exercise);
        Task<bool> DeleteExerciseAsync(int exerciseId);
    }

    public class DataService : IDataService
    {
        private readonly ObservableCollection<Model> _models;
        private readonly ObservableCollection<TrainingSession> _trainingSessions;
        private readonly ObservableCollection<TrainingProgram> _trainingPrograms;
        private readonly ObservableCollection<Exercise> _exercises;
        private int _nextModelId = 1;
        private int _nextSessionId = 1;
        private int _nextProgramId = 1;
        private int _nextExerciseId = 1;

        public DataService()
        {
            _models = new ObservableCollection<Model>();
            _trainingSessions = new ObservableCollection<TrainingSession>();
            _trainingPrograms = new ObservableCollection<TrainingProgram>();
            _exercises = new ObservableCollection<Exercise>();
            // Initialize with sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Sample Models
            var models = new[]
            {
                new Model { Id = _nextModelId++, Name = "LM-1", Type = "Language Model", Dataset = "WikiText", Status = "Idle", Accuracy = 0.0, Loss = 0.0, LastRun = null },
                new Model { Id = _nextModelId++, Name = "VisionNet", Type = "Vision Model", Dataset = "ImageNet", Status = "Idle", Accuracy = 0.0, Loss = 0.0, LastRun = null }
            };
            foreach (var model in models)
            {
                _models.Add(model);
            }

            // Sample Exercises (Reinforcement steps)
            var exercises = new[]
            {
                new Exercise { Id = _nextExerciseId++, Name = "Reward Step 1", Description = "Reward for correct answer", RewardType = "Positive", RewardValue = 1.0, Action = "Answer", Observation = "Correct", Status = "Completed", Executed = DateTime.Now },
                new Exercise { Id = _nextExerciseId++, Name = "Penalty Step 1", Description = "Penalty for wrong answer", RewardType = "Negative", RewardValue = -1.0, Action = "Answer", Observation = "Incorrect", Status = "Completed", Executed = DateTime.Now }
            };
            foreach (var exercise in exercises)
            {
                _exercises.Add(exercise);
            }

            // Sample Training Programs
            var programs = new[]
            {
                new TrainingProgram { Id = _nextProgramId++, Name = "LM Pretraining", Description = "Pretraining for language model", Objective = "Learn language structure", Dataset = "WikiText", Status = "Draft", Created = DateTime.Today, LastRun = null },
                new TrainingProgram { Id = _nextProgramId++, Name = "VisionNet Training", Description = "Training for vision model", Objective = "Image classification", Dataset = "ImageNet", Status = "Draft", Created = DateTime.Today, LastRun = null }
            };
            foreach (var program in programs)
            {
                _trainingPrograms.Add(program);
            }

            // Sample Training Sessions
            var sessions = new[]
            {
                new TrainingSession { Id = _nextSessionId++, Name = "LM-1 Run 1", Description = "First run of LM-1", Status = "Completed", Scheduled = DateTime.Today.AddHours(8), Started = DateTime.Today.AddHours(8), Ended = DateTime.Today.AddHours(10), Duration = TimeSpan.FromHours(2), ModelType = "Language Model", Dataset = "WikiText", Accuracy = 0.85, Loss = 0.35 },
                new TrainingSession { Id = _nextSessionId++, Name = "VisionNet Run 1", Description = "First run of VisionNet", Status = "Completed", Scheduled = DateTime.Today.AddHours(12), Started = DateTime.Today.AddHours(12), Ended = DateTime.Today.AddHours(14), Duration = TimeSpan.FromHours(2), ModelType = "Vision Model", Dataset = "ImageNet", Accuracy = 0.92, Loss = 0.18 }
            };
            foreach (var session in sessions)
            {
                _trainingSessions.Add(session);
            }
        }

        // Model CRUD
        public async Task<ObservableCollection<Model>> GetModelsAsync()
        {
            await Task.Delay(100);
            return _models;
        }

        public async Task<Model> AddModelAsync(Model model)
        {
            await Task.Delay(50);
            model.Id = _nextModelId++;
            _models.Add(model);
            return model;
        }

        public async Task<bool> UpdateModelAsync(Model model)
        {
            await Task.Delay(50);
            var existing = _models.FirstOrDefault(m => m.Id == model.Id);
            if (existing != null)
            {
                var index = _models.IndexOf(existing);
                _models[index] = model;
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteModelAsync(int modelId)
        {
            await Task.Delay(50);
            var model = _models.FirstOrDefault(m => m.Id == modelId);
            if (model != null)
            {
                _models.Remove(model);
                return true;
            }
            return false;
        }

        // TrainingSession CRUD
        public async Task<ObservableCollection<TrainingSession>> GetTrainingSessionsAsync()
        {
            await Task.Delay(100);
            return _trainingSessions;
        }

        // TrainingProgram CRUD
        public async Task<ObservableCollection<TrainingProgram>> GetTrainingProgramsAsync()
        {
            await Task.Delay(100);
            return _trainingPrograms;
        }

        public async Task<TrainingProgram> AddTrainingProgramAsync(TrainingProgram program)
        {
            await Task.Delay(50);
            program.Id = _nextProgramId++;
            _trainingPrograms.Add(program);
            return program;
        }

        public async Task<bool> UpdateTrainingProgramAsync(TrainingProgram program)
        {
            await Task.Delay(50);
            var existing = _trainingPrograms.FirstOrDefault(p => p.Id == program.Id);
            if (existing != null)
            {
                var index = _trainingPrograms.IndexOf(existing);
                _trainingPrograms[index] = program;
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTrainingProgramAsync(int programId)
        {
            await Task.Delay(50);
            var program = _trainingPrograms.FirstOrDefault(p => p.Id == programId);
            if (program != null)
            {
                _trainingPrograms.Remove(program);
                return true;
            }
            return false;
        }

        // Exercise CRUD
        public async Task<ObservableCollection<Exercise>> GetExercisesAsync()
        {
            await Task.Delay(100);
            return _exercises;
        }

        public async Task<Exercise> AddExerciseAsync(Exercise exercise)
        {
            await Task.Delay(50);
            exercise.Id = _nextExerciseId++;
            _exercises.Add(exercise);
            return exercise;
        }

        public async Task<bool> UpdateExerciseAsync(Exercise exercise)
        {
            await Task.Delay(50);
            var existing = _exercises.FirstOrDefault(e => e.Id == exercise.Id);
            if (existing != null)
            {
                var index = _exercises.IndexOf(existing);
                _exercises[index] = exercise;
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteExerciseAsync(int exerciseId)
        {
            await Task.Delay(50);
            var exercise = _exercises.FirstOrDefault(e => e.Id == exerciseId);
            if (exercise != null)
            {
                _exercises.Remove(exercise);
                return true;
            }
            return false;
        }

        public async Task<TrainingSession> AddTrainingSessionAsync(TrainingSession session)
        {
            await Task.Delay(50);
            session.Id = _nextSessionId++;
            _trainingSessions.Add(session);
            return session;
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
