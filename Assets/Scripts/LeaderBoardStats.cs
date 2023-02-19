using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderBoardStats : MonoBehaviour
{
    [Header("Leader Board")]
    [SerializeField] private LeaderboardYG _leaderboard;

    [Header("Leader Board Settings")]
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _clipButton;

    private Character _currentBestCharater;

    private void OnEnable()
    {
        _clipButton.onClick.AddListener(ClipButtonClick);
    }

    private void OnDisable()
    {
        _clipButton.onClick.RemoveListener(ClipButtonClick);
    }

    public void UpdateLeaderBoard(Character bestCharater)
    {
        _leaderboard.NewScore(bestCharater.Level);
        _leaderboard.UpdateLB();

        _currentBestCharater = bestCharater;
        UpdateView();
    }

    private void UpdateView()
    {
        _nameText.text = $"{_currentBestCharater.Name}";
        _levelText.text = $"{_currentBestCharater.Level}";

        _iconImage.sprite = _currentBestCharater.Icon;
    }

    private void ClipButtonClick()
    {
        AudioManager.Instance.CharacterClip(_currentBestCharater.Clip);
    }
}
