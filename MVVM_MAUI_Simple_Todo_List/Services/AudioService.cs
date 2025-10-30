using Plugin.Maui.Audio;

namespace MVVM_MAUI_Simple_Todo_List.Services;

public class AudioService : IDisposable
{
    private IAudioPlayer? _player;
    private readonly IAudioManager _audioManager;
    private readonly SettingsService _settingsService; // Для настроек звука

    // Внедряем AudioManager и SettingsService
    public AudioService(IAudioManager audioManager, SettingsService settingsService)
    {
        _audioManager = audioManager;
        _settingsService = settingsService;
        // Позже добавим сюда настройку вкл/выкл звука
    }

    // Метод для воспроизведения звука добавления задачи
    public async Task PlayAddSoundAsync()
    {
        try
        {
            // Если звук выключен в настройках, выходим
            // (Мы добавим свойство IsSoundEnabled в SettingsService позже)
            // if (!_settingsService.IsSoundEnabled) return;

            // Загружаем и проигрываем звук
            // Убедись, что имя файла совпадает!
            _player?.Dispose(); // Освобождаем предыдущий плеер, если был
            _player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("motivation.mp3"));

            if (_player != null)
            {
                _player.Play();
            }
        }
        catch (Exception ex)
        {
            // Обработка ошибки (например, файл не найден)
            System.Diagnostics.Debug.WriteLine($"Ошибка воспроизведения звука: {ex.Message}");
        }
    }

    // Реализация IDisposable для освобождения ресурсов плеера
    public void Dispose()
    {
        _player?.Dispose();
    }
}