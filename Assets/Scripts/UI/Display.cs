using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class Display : MonoBehaviour
{
    private Canvas _canvas;
    private Animator _anim;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _anim = GetComponent<Animator>();

        _canvas.enabled = false;
    }

    public void Show()
    {
        _canvas.enabled = true;

        _anim.SetTrigger("Show");
    }

    public void Hide()
    {
        _anim.SetTrigger("Hide");

        StartCoroutine(WaitAnim());
    }

    private IEnumerator WaitAnim()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        _canvas.enabled = false;
    }
}
