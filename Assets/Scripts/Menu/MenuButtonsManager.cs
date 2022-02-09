using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;
    [Header("Animation Setings")]
    public float duration = .5f;
    public float delay = .5f;
    public Ease ease;

    private void OnEnable()
    {
        AnimationButtons();
    }

    private void Awake()
    {
        HideButtons();
    }

    private void HideButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(false);
            button.transform.localScale = Vector3.zero;
        }
    }

    private void AnimationButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            button.SetActive(true);
            button.transform.DOScale(1, duration).SetEase(ease).SetDelay(i * delay);
        }
    }
}
