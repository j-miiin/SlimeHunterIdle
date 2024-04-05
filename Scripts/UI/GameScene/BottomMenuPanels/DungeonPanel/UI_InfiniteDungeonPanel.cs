using UnityEngine;
using UnityEngine.UI;

public class UI_InfiniteDungeonPanel : UI_Base
{
    [SerializeField] private RecyclerScroll _recyclerScroll;
    [SerializeField] private Button _enterButton;
    [SerializeField] private Button _closeButtonPanel;

    private InfiniteDungeon _infiniteDungeon;

    public override void Init()
    {
        base.Init();
        _infiniteDungeon = DungeonManager.Instance.GetDungeon(Enums.DungeonType.Infinite) as InfiniteDungeon;
        _recyclerScroll.Init();
        _closeButtonPanel.onClick.AddListener(CloseUI);
    }

    public override void OpenUI()
    {
        base.OpenUI();
        _recyclerScroll.SetScrollPosition(_infiniteDungeon.CurHighestStage - 1);
    }
}
