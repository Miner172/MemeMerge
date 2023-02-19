using UnityEngine;
using UnityEngine.UI;

public class LevelDisplayController : MonoBehaviour
{
    [Header("Displays")]
    [SerializeField] private Display _pause;
    [SerializeField] private Display _win;
    [SerializeField] private Display _lose;
    [SerializeField] private Display _infinity;

    [Header("Buttons")]
    [SerializeField] private Button _openPauseButton;
    [SerializeField] private Button _closePauseButton;

    private void OnEnable()
    {
        _openPauseButton.onClick.AddListener(OpenPause);
        _closePauseButton.onClick.AddListener(ClosePause);
    }

    private void OnDisable()
    {
        _openPauseButton.onClick.RemoveListener(OpenPause);
        _closePauseButton.onClick.RemoveListener(ClosePause);
    }

    public void OpenPause()
    {
        _pause.Show();
        Time.timeScale = 0;
    }

    public void ClosePause()
    {
        _pause.Hide();
        Time.timeScale = 1;
    }

    public void Lose()
    {
        _pause.Hide();
        _lose.Show();
    }

    public void Victory()
    {
        _pause.Hide();
        _win.Show();
    }

    public void Infinity()
    {
        _pause.Hide();
        _infinity.Show();
    }
}
