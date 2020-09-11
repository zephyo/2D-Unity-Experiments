using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using CharTween;
using TMPro;

public class TextSceneExample : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] strings = new string[]{
        "are you alone?",
        "are you scared?",
        "are you afraid?"
    };

    public float delay = 3;

    #region PRIVATE
    private int _index = 0;
    private CharTweener _tweener;
    #endregion
    private void Start()
    {
        _tweener = text.GetCharTweener();
        PlayString(_index, PlayNextString);
    }


    private void PlayNextString()
    {
        StartCoroutine(PlayNextStringWait());
    }

    private IEnumerator PlayNextStringWait()
    {
        yield return new WaitForSeconds(delay);
        _index++;
        if (_index > strings.Length - 1) _index = 0;
        PlayString(_index, PlayNextString);
    }

    private void PlayString(int index, Action onComplete)
    {
        text.text = strings[index];
        TweenBubble(0, _tweener.CharacterCount, 0.3f, 2.5f, onComplete);
    }

    // Char tween: bubbly fade-in + bounce
    private void TweenBubble(int start, int end, float duration, float scale, Action onComplete)
    {
        var sequence = DOTween.Sequence();

        for (var i = start; i < end; ++i)
        {
            var timeOffset = Mathf.Lerp(0, duration, (i - start) / (float)(end - start + duration));
            var charSequence = DOTween.Sequence();
            charSequence.Append(_tweener.DOLocalMoveY(i, 0.5f, duration).SetEase(Ease.InOutCubic))
                .Join(_tweener.DOFade(i, 0, duration).From())
                .Join(_tweener.DOScale(i, 0, duration).From().SetEase(Ease.OutBack, scale))
                .Append(_tweener.DOLocalMoveY(i, 0, duration).SetEase(Ease.OutBounce));
            sequence.Insert(timeOffset, charSequence);
        }
        sequence.Play().AppendCallback(() => onComplete());
    }
}
