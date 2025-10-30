using Plugin.Maui.Audio;

namespace MVVM_MAUI_Simple_Todo_List.Services;

public class AudioService : IDisposable
{
    private IAudioPlayer? _player;
    private readonly IAudioManager _audioManager;
    private readonly SettingsService _settingsService; // For sound settings
    
    public AudioService(IAudioManager audioManager, SettingsService settingsService)
    {
        _audioManager = audioManager;
        _settingsService = settingsService;
    }

    // Method for sound
    public async Task PlayAddSoundAsync()
    {
        try
        {
            _player?.Dispose();
            _player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("motivation.mp3"));

            if (_player != null)
            {
                _player.Play();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
        }
    }
    
    public void Dispose()
    {
        _player?.Dispose();
    }
}