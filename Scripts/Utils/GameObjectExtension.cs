using System;
using DG.Tweening;
using UnityEngine;

public static class GameObjectExtension
{
    public static Sequence ScaleUp(this GameObject target, Action endAction = null)
    {
        if (!target.TryGetComponent(out RectTransform rect)) return null;

        target.transform.localScale = Vector3.zero;

        Sequence animation = DOTween.Sequence();

        animation.Append(rect.DOScale(1, 0.2f));
        if (endAction != null) animation.OnComplete(() => endAction());

        return animation.SetUpdate(true).Play();
    }

    public static Sequence ScaleDown(this GameObject target, Action endAction = null)
    {
        if (!target.TryGetComponent(out RectTransform rect)) return null;

        target.transform.localScale = Vector3.one;

        Sequence animation = DOTween.Sequence();

        animation.Append(rect.DOScale(0, 0.2f));
        if (endAction != null) animation.OnComplete(() => endAction());

        return animation.SetUpdate(true).Play();
    }

    public static Sequence FadeIn(this GameObject target, Action endAction = null)
    {
        if (!target.TryGetComponent(out CanvasGroup group)) return null;

        group.alpha = 0f;

        Sequence animation = DOTween.Sequence();

        animation.Append(group.DOFade(1, 0.5f));
        if (endAction != null) animation.OnComplete(() => endAction());

        target.SetActive(true);
        return animation.SetUpdate(true).Play();
    }

    public static Sequence FadeOut(this GameObject target, Action endAction = null)
    {
        if (!target.TryGetComponent(out CanvasGroup group)) return null;

        group.alpha = 1f;

        Sequence animation = DOTween.Sequence();

        animation.Append(group.DOFade(0, 0.5f));
        if (endAction != null) animation.OnComplete(() => endAction());

        return animation.SetUpdate(true).Play();
    }

    public static Sequence FadeOutDamageEffect(this GameObject target, Action endAction = null)
    {
        if (!target.TryGetComponent(out CanvasGroup group) || !target.TryGetComponent(out RectTransform rect)) return null;

        target.transform.localScale = Vector3.zero;
        group.alpha = 1f;

        Sequence animation = DOTween.Sequence();

        animation.Append(rect.DOScale(1, 0.5f));
        animation.Append(group.DOFade(0, 0.4f));
        if (endAction != null) animation.OnComplete(() => endAction());

        return animation.SetUpdate(true).Play();
    }
}
