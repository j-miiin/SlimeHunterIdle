using System;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_DungeonSlot : UI_Base
{
    public event Action<DungeonType> OnClickEnterButton;

    public DungeonType Type;
    [SerializeField] private Image _iconSprite;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Button _enterButton;

    private Dungeon _dungeon;

    private void Awake()
    {
        _enterButton.onClick.AddListener(() => { OnClickEnterButton?.Invoke(Type); });
    }

    public void RefreshSlot(Dungeon dungeon)
    {
        _dungeon = dungeon;
        DungeonDataSO data = _dungeon.DataSO;
        _iconSprite.sprite = data.Icon;
        _nameText.text = data.DungeonName;
        _descriptionText.text = data.Description;
    }
}
