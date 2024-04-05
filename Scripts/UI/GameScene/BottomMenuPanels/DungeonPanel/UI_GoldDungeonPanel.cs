using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_GoldDungeonPanel : UI_Base
{
    [SerializeField] private Text _stageText;
    [SerializeField] private UI_RewardSlot[] _rewardsList;
    [SerializeField] private Button _enterButton;
    [SerializeField] private Button _clearButton;
    [SerializeField] private Button _closeButton;

    private DungeonManager _dungeonManager;
    private GoldDungeon _goldDungeon;

    public override void Init()
    {
        base.Init();
        _dungeonManager = DungeonManager.Instance;
        _goldDungeon = _dungeonManager.GetDungeon(DungeonType.Gold) as GoldDungeon;

        _enterButton.onClick.AddListener(() => { _dungeonManager.EnterDungeon(DungeonType.Gold); });
        _clearButton.onClick.AddListener(() => { _dungeonManager.ClearDungeon(DungeonType.Gold); });
        _closeButton.onClick.AddListener(CloseUI);
    }

    public override void OpenUI()
    {
        base.OpenUI();
        Refresh();
    }

    public void Refresh()
    {
        _stageText.text = $"{_goldDungeon.CurHighestStage}´Ü°è";

        List<Reward> rewards = _goldDungeon.GetHighestStageRewards();
        for (int i = 0; i < rewards.Count; i++) 
        {
            _rewardsList[i].Refresh(rewards[i]);
        }
    }
}
