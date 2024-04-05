using UnityEngine;

public class GameObjectActiveAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject _obj;

    public void ActiveObject()
    {
        _obj.SetActive(true);
    }

    public void InActiveObject()
    {
        _obj.SetActive(false);
    }
}
