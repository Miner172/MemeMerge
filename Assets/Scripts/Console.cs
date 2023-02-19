using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Console : MonoBehaviour
{
    [SerializeField] private Text _consoleText;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void MessageToConsole(string message)
    {
        AudioManager.Instance.Erorr();

        _consoleText.text = message;

        _anim.SetTrigger("Show");
    }
}
