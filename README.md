
# Training Control Panel Dashboard

A cross-platform .NET MAUI application for managing machine learning model training, orchestration, and analytics for LMs and other ML models.

## Features

- **Model Management** - Track and manage ML model profiles, types, datasets, and training status
- **Training Programs** - Create and manage ML training pipelines and program metadata
- **Training Sessions** - Schedule, monitor, and analyze model training runs
- **Analytics** - View model performance metrics, training KPIs, and trends
- **Settings** - Configure application and integration preferences

## Screenshots

*Coming soon...*

## Requirements

- .NET 8.0 or later
- Visual Studio 2022 with .NET MAUI workload installed

## Supported Platforms

- ✅ Windows 10/11 (version 10.0.17763.0 or later)
- ✅ Android (API 21 or later)
- ✅ iOS (11.0 or later)
- ✅ macOS (macCatalyst 13.1 or later)

## Getting Started

### Prerequisites

1. Install [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) with the following workloads:
   - .NET Multi-platform App UI development
   - .NET desktop development

2. Ensure you have the latest .NET 8.0 SDK installed

### Building and Running

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd TrainingControlPanelDashboard
   ```

2. Open the solution in Visual Studio:
   ```
   TrainingControlPanelDashboard.sln
   ```

3. Select your target platform (Windows, Android, iOS, etc.)

4. Build and run the application:
   - Press `F5` to run with debugging
   - Or press `Ctrl+F5` to run without debugging

## Project Structure

```
TrainingControlPanelDashboard/
├── Models/              # Data models (Model, TrainingSession, TrainingProgram, Exercise, etc.)
├── Pages/               # XAML pages for different dashboard and management views
├── ViewModels/          # MVVM view models
├── Services/            # Business logic and data services (DVC, Langraph, etc.)
├── Resources/           # Images, fonts, styles, and other resources
├── Platforms/           # Platform-specific code
└── Properties/          # Launch settings and configuration
```

## Architecture

This application follows the MVVM (Model-View-ViewModel) architectural pattern:

- **Models**: Data structures representing ML models, training sessions, training programs, and RL exercises
- **Views**: XAML pages for user interface (dashboard, models, sessions, analytics, etc.)
- **ViewModels**: Business logic and data binding between models and views
- **Services**: Data access, orchestration, and integration with ML tools (DVC, Langraph, etc.)

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Changelog

See [CHANGELOG.txt](CHANGELOG.txt) for a list of changes and version history.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For questions or support, please open an issue on this repository.

## Acknowledgments

- Built with [.NET MAUI](https://dotnet.microsoft.com/apps/maui)
- Icons and images from [appropriate attribution if applicable]
