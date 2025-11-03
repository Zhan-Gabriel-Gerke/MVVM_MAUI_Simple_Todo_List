MVVM MAUI Simple Todo List
This is a simple cross-platform Todo List application built with .NET MAUI. The project demonstrates the use of the MVVM pattern, storing data in a local SQLite database, and the implementation of additional features like localization and theme switching.

✨ Features
Task Management:

Adding new tasks.

Deleting existing tasks.

Marking tasks as completed (with visual strikethrough text).

Local Storage:

All tasks are saved to a local SQLite database (todo.db3), ensuring persistence between sessions.

Drag & Drop:

Ability to reorder tasks in the list by dragging and dropping.

Personalization (on the "Settings" page):

Theme Switching: Choose between light, dark, and system default themes.

Localization: Support for three languages: English (en-US), Estonian (et-EE), and Russian (ru-RU).

Audio:

Plays a sound effect (motivation.mp3) when a new task is added.

🛠️ Technology and Architecture
Framework: .NET MAUI (project configured for Android, iOS, MacCatalyst, and Windows).

Pattern: MVVM (Model-View-ViewModel) using CommunityToolkit.Mvvm.

Database: Local SQLite storage using sqlite-net-pcl.

Audio: Plugin.Maui.Audio for cross-platform audio playback.

Dependency Injection (DI): Used to register and inject services into ViewModels.

📂 Project Structure
The project is organized according to the MVVM pattern:

/Models: Contains data model classes (TodoItem.cs).

/Views: Contains user interface XAML pages (MainPage.xaml, SettingsPage.xaml).

/ViewModels: Contains presentation logic and state (MainPageViewModel.cs, SettingsViewModel.cs).

/Services: Contains business logic and services:

DatabaseService.cs: Manages all SQLite operations.

ThemeService.cs: Manages the application of themes (light/dark).

LocalizationService.cs: Manages loading localization strings.

SettingsService.cs: Stores user settings (selected theme and language).

AudioService.cs: Responsible for playing sounds.

/Resources: Application resources, including fonts, images, styles, and localization files.
