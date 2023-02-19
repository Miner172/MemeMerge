using UnityEngine;

public class AudioAppeal : MonoBehaviour
{
    public void ButtonClick()
    {
        AudioManager.Instance.ButtonClick();
    }

    public void ShowSettingsDisplay()
    {
        AudioManager.Instance.ShowSettingsDisplay();
    }
}
