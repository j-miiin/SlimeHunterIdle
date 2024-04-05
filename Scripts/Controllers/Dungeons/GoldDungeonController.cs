using System;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class GoldDungeonController : DungeonController
{
    public event Action<int> OnStageUpdate;
    public event Action<int, int> OnKilledMonsterCountUpdate;

    [Header("한 스테이지 당 필요한 처치 몬스터 수")]
    public int MonsterPerStageCount;

    private DungeonManager _dungeonManager;
    private RewardManager _rewardManager;
    private InfiniteWaveSystem _goldDungeonSystem;
    private GoldDungeon _goldDungeon;

    private int _curStage = 1;

    public override void StartDungeon()
    {
        if (!_dungeonManager) _dungeonManager = DungeonManager.Instance;
        if (!_rewardManager) _rewardManager = RewardManager.Instance;
        if (!_goldDungeonSystem)
        {
            GameObject obj = ResourceManager.Instance.Instantiate(Strings.Prefabs.GOLD_DUNGEON_WAVE_SYSTEM);
            _goldDungeonSystem = obj.GetComponent<InfiniteWaveSystem>();
        }
        if (_goldDungeon == null) _goldDungeon = _dungeonManager.GetDungeon(DungeonType.Gold) as GoldDungeon;

        _goldDungeonSystem.OnMonsterKilled += UpdateMonsterCount;

        _goldDungeonSystem.StartWave();
        StartTimer(_goldDungeon.DataSO.Time);
    }

    public override void EndDungeon()
    {
        _goldDungeon.UpdateHighestStage(_curStage);
        _goldDungeonSystem.ClearMonsters();
        List<Reward> rewards = _goldDungeon.GetHighestStageRewards();
        UIManager.Instance.ShowPopup<UI_NotifyRewardPopup>(
            new NotifyRewardPopupParameter(
                rewardList: rewards,
                Strings.Popup.NOTIFY_REWARD_POPUP_CONTENT
                ));
        foreach (Reward reward in rewards)
        {
            _rewardManager.GiveReward(reward.Type, reward.Value);
        }
        Invoke("Exit", 2f);
    }

    public void Exit()
    {
        SceneManagerEx.Instance.LoadScene(Scenes.GameScene);
    }

    private void UpdateMonsterCount(int killMonsterCount)
    {
        int stage = killMonsterCount / MonsterPerStageCount + 1;
        int value = killMonsterCount % MonsterPerStageCount;

        OnKilledMonsterCountUpdate?.Invoke(value, killMonsterCount);

        if (stage > _curStage)
        {
            _curStage = stage;
            OnStageUpdate?.Invoke(_curStage);
        }
    }
}
