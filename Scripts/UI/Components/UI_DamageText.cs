using DG.Tweening;
using Keiwando.BigInteger;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamageText : UI_Base
{
    [SerializeField] private GameObject _damageText;

    private ResourceManager _resourceManager;

    private bool _isInit;

    public override void Init()
    {
        base.Init();
        _isInit = true;
        _resourceManager = ResourceManager.Instance;
    }

    public void ShowDamage(BigInteger damage, Vector3 position)
    {
        if (!_isInit) Init();
        GameObject obj = _resourceManager.Instantiate(_damageText, parent: transform);
        if (obj)
        {
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<Text>().text = damage.ChangeMoney();
            obj.transform.position = new Vector3(position.x, position.y + 0.5f);
        }
        obj.FadeOutDamageEffect(endAction: () => { _resourceManager.Destroy(obj); });
    }
}
