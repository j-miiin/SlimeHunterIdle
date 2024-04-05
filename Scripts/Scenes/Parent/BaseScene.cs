using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public Scenes SceneType = Scenes.Unknown;

    protected UIManager UIManager;
    private PoolManager _poolManager;

    protected bool _init = false;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init) return false;

        _init = true;
        Object go = FindObjectOfType(typeof(EventSystem));
        if (go == null) Instantiate(Resources.Load(Strings.Prefabs.UI_EVENTSYSTEM));
        UIManager = UIManager.Instance;
        _poolManager = PoolManager.Instance;

        return true;
    }

    public virtual void Clear()
    {
        UIManager.RemoveAllUIComponent();
        _poolManager.Clear();
    }
}
