using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecyclerScroll : MonoBehaviour
{
    [SerializeField] private UI_InfiniteDungeonSlot _prefab;
    [SerializeField] private float _itemHeight;

    private float _offset;
    private float _scrollHeight;
    private ScrollRect _scroll;
    private RectTransform _scrollRect;
    private List<int> _dataList = new List<int>();
    private List<UI_InfiniteDungeonSlot> _itemList;

    private void Awake()
    {
        _scroll = GetComponent<ScrollRect>();
        _scrollRect = _scroll.GetComponent<RectTransform>();
        _scrollHeight = _scrollRect.rect.height;
    }

    public void Init()
    {
        for(int i = 0; i < 100; i++)
        {
            _dataList.Add(i);
        }
        CreateItem();
        SetContentHeight();
    }

    private void Update()
    {
        float contentY = _scroll.content.anchoredPosition.y;
        foreach (UI_InfiniteDungeonSlot item in _itemList)
        {
            bool isChanged = RelocationItem(item.gameObject, contentY);
            if (isChanged)
            {
                int idx = (int)(-item.transform.localPosition.y / _itemHeight);
                SetData(item, idx);
            }
        }
    }

    public void SetScrollPosition(int idx)
    {
        _scroll.content.anchoredPosition = new Vector2(_scroll.content.anchoredPosition.x, idx * _itemHeight);
    }

    private void CreateItem()
    {
        _itemList = new List<UI_InfiniteDungeonSlot>();

        int itemCount = (int)(_scrollHeight / _itemHeight) + 3;
        for (int i = 0; i < itemCount; i++)
        {
            UI_InfiniteDungeonSlot item = Instantiate(_prefab, _scroll.content);
            _itemList.Add(item);
            item.transform.localPosition = new Vector3(0, -i * _itemHeight);
            SetData(item, i);
        }
        _offset = _itemList.Count * _itemHeight;
    }

    private void SetContentHeight()
    {
        _scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, _dataList.Count * _itemHeight);
    }

    private bool RelocationItem(GameObject obj, float contentY)
    {
        if (obj.transform.localPosition.y + contentY > _itemHeight * 2f)
        {
            obj.transform.localPosition -= new Vector3(0, _offset);
            RelocationItem(obj, contentY);
            return true;
        } 
        else if (obj.transform.localPosition.y + contentY < -_scrollHeight - _itemHeight)
        {
            obj.transform.localPosition += new Vector3(0, _offset);
            RelocationItem(obj, contentY);
            return true;
        }
        return false;
    }

    private void SetData(UI_InfiniteDungeonSlot item, int idx)
    {
        if (idx < 0 || idx >= _dataList.Count)
        {
            item.gameObject.SetActive(false);
            return;
        }
        item.gameObject.SetActive(true);
        item.Refresh(_dataList[idx]);
    }
}
