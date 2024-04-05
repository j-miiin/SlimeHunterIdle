using UnityEngine;
using static Enums;

public class UI_Base : MonoBehaviour
{
    [SerializeField] protected GameObject _animationTarget;
    private bool _isInit;

    public virtual void Init()
    {
        _isInit = true;
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
            canvas.sortingLayerID = SortingLayer.NameToID(Strings.SortingLayer.UI_CANVAS_LAYER_NAME);
        }
    }

    public virtual void OpenUI()
    {
        if (!_isInit) Init();
        gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public virtual void Clear()
    {
    }

    public virtual void OpenUI(UIAnimationType animationType, bool isOpen = true)
    {
        if (_animationTarget == null) return;
        if (isOpen) gameObject.SetActive(true);
        _animationTarget.SetActive(true);
        switch (animationType)
        {
            case UIAnimationType.None:
                break;
            case UIAnimationType.Scale:
                _animationTarget.ScaleUp();
                break;
            case UIAnimationType.Fade:
                _animationTarget.FadeIn();
                break;
        }
    }

    public virtual void CloseUI(UIAnimationType animationType, bool isClose = true)
    {
        if (_animationTarget == null) return;
        if (isClose) gameObject.SetActive(false);
        _animationTarget.SetActive(false);
        switch (animationType)
        {
            case UIAnimationType.None:
                break;
            case UIAnimationType.Scale:
                _animationTarget.ScaleDown();
                break;
            case UIAnimationType.Fade:
                _animationTarget.FadeOut();
                break;
        }
    }

    private void OnDestroy()
    {
        Clear();
    }
}
