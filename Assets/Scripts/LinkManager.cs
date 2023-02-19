using UnityEngine;

public class LinkManager : MonoBehaviour
{
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void MoreGames()
    {
        Application.OpenURL(@"https://yandex.ru/games/developer?name=MinerKa");
    }
}
