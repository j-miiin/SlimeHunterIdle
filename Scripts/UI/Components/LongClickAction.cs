using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private WaitForSeconds _waitTime = new WaitForSeconds(1f);
    private WaitForSeconds _clickInterval = new WaitForSeconds(0.2f);

    private Button _button;
    private Coroutine _curCoroutine;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _curCoroutine = StartCoroutine(COUpgradeAction());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_curCoroutine != null)
        {
            StopCoroutine(_curCoroutine);
            _curCoroutine = null;
        }
    }

    private IEnumerator COUpgradeAction()
    {
        yield return _waitTime;
        while (true)
        {
            if (_button != null) _button.onClick.Invoke();
            yield return _clickInterval;
        }
    }
}
