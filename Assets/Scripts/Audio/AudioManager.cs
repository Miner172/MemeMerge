using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioSource _music;
    [SerializeField] private Display _settingsDisplay;
    [SerializeField] private Button _hideSettingsDisplayButton;

    [Header("AudioComponents")]
    [SerializeField] private Slider _soundSlider; 
    [SerializeField] private Slider _musicSlider; 

    [Header("Volume")]
    [SerializeField] private float _buttonClickVolume = 1;
    [SerializeField] private float _sellVolume = 1;
    [SerializeField] private float _swapCharactersVolume = 1;
    [SerializeField] private float _errorVolume = 1;
    [SerializeField] private float _startLevelVolume = 1;
    [SerializeField] private float _goHomeVolume = 1;
    [SerializeField] private float _victoryVolume = 1;
    [SerializeField] private float _loseVolume = 1;
    [SerializeField] private float _completeGameVolume = 1;
    [SerializeField] private float _addCoinsVolume = 1;
    [SerializeField] private float _addLightingsVolume = 1;
    [SerializeField] private float _spawnCharaterVolume = 1;
    [SerializeField] private float _lossCoinsVolume = 1;
    [SerializeField] private float _completeLevelVolume = 1;
    [SerializeField] private float _characterClipsVolume = 1;
    [SerializeField] private float _backgroundVolume = 1;

    [Header("Audio")]
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _sell;
    [SerializeField] private AudioClip _swapCharacters;
    [SerializeField] private AudioClip _error;
    [SerializeField] private AudioClip _startLevel;
    [SerializeField] private AudioClip _goHome;
    [SerializeField] private AudioClip _victory;
    [SerializeField] private AudioClip _lose;
    [SerializeField] private AudioClip _completeGame;
    [SerializeField] private AudioClip _addCoins;
    [SerializeField] private AudioClip _addLightings;
    [SerializeField] private AudioClip _spawnCharacter;
    [SerializeField] private AudioClip _lossCoins;
    [SerializeField] private AudioClip _completeLevel;
    [SerializeField] private AudioClip[] _backgroundMusic;

    public static AudioManager Instance;

    private void OnEnable()
    {
        _hideSettingsDisplayButton.onClick.AddListener(HideSettingsDisplay);
        _soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    private void OnDisable()
    {
        _hideSettingsDisplayButton.onClick.RemoveListener(HideSettingsDisplay);
        _soundSlider.onValueChanged.RemoveListener(ChangeSoundVolume);
        _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
    }

    private void Start()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            Init();
        }
    }

    private void Init()
    {
        StartCoroutine(BackroundSound());
    }

    public void ChangeSoundVolume(float volume)
    {
        _sound.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        _music.volume = volume;
    }

    public void ShowSettingsDisplay()
    {
        _settingsDisplay.Show();

        Time.timeScale = 0;
    }

    private void HideSettingsDisplay()
    {
        _settingsDisplay.Hide();

        Time.timeScale = 1;
    }

    public void ButtonClick()
    {
        _sound.PlayOneShot(_buttonClick, _buttonClickVolume);
    }

    public void Sell()
    {
        _sound.PlayOneShot(_sell, _sellVolume);
    }

    public void Swap()
    {
        _sound.PlayOneShot(_swapCharacters, _swapCharactersVolume);
    }

    public void Erorr()
    {
        _sound.PlayOneShot(_error, _errorVolume);
    }

    public void StartLevel()
    {
        _sound.PlayOneShot(_startLevel, _startLevelVolume);
    }

    public void GoHome()
    {
        _sound.PlayOneShot(_goHome, _goHomeVolume);
    }

    public void Victory()
    {
        _sound.PlayOneShot(_victory, _victoryVolume);
    }

    public void Lose()
    {
        _sound.PlayOneShot(_lose, _loseVolume);
    }

    public void CompleteGame()
    {
        _sound.PlayOneShot(_completeGame, _completeGameVolume);
    }

    public void AddCoins()
    {
        _sound.PlayOneShot(_addCoins, _addCoinsVolume);
    }

    public void AddLightings()
    {
        _sound.PlayOneShot(_addLightings, _addLightingsVolume);
    }

    public void SpawnCharacter()
    {
        _sound.PlayOneShot(_spawnCharacter, _spawnCharaterVolume);
    }

    public void LossCoins()
    {
        _sound.PlayOneShot(_lossCoins, _lossCoinsVolume);
    }

    public void CompleteLevel()
    {
        _sound.PlayOneShot(_completeLevel, _completeLevelVolume);
    }

    public void CharacterClip(AudioClip clip)
    {
        _sound.PlayOneShot(clip, _characterClipsVolume);
    }

    private IEnumerator BackroundSound()
    {
        int randNum = Random.Range(0, _backgroundMusic.Length);
        _music.PlayOneShot(_backgroundMusic[randNum], _backgroundVolume);

        yield return new WaitForSecondsRealtime(_backgroundMusic[randNum].length);

        StartCoroutine(BackroundSound());
    }
}
