using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Strings.Prefabs;

public class UI_InfiniteDungeonSlot : UI_Base
{
    [SerializeField] private Text _stageText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Transform _rewardsContainer;
    [SerializeField] private Outline _outline;
    [SerializeField] private GameObject _clearImage;

    private Color _outlineColor;

    private ResourceManager _resourceManager;
    private InfiniteDungeon _infiniteDungeon;
    private int _idx;

    private bool _isInit;

    public override void Init()
    {
        _resourceManager = ResourceManager.Instance;    
        _infiniteDungeon = DungeonManager.Instance.GetDungeon(Enums.DungeonType.Infinite) as InfiniteDungeon;
        _outlineColor = _outline.effectColor;
    }

    public void Refresh(int idx)
    {
        if (!_isInit) Init();

        ClearRewardSlots();

        _idx = idx;
        _stageText.text = $"{_idx + 1}´Ü°è";

        List<Reward> rewards = _infiniteDungeon.GetStageRewards(_idx + 1);
        foreach (Reward reward in rewards)
        {
            GameObject obj = _resourceManager.Instantiate(UI_REWARD_SLOT_PATH, parent: _rewardsContainer);
            obj.transform.localScale = Vector3.one;
            if (obj && obj.TryGetComponent(out UI_RewardSlot slot))
            {
                slot.Refresh(reward);
            }
        }

        _outlineColor.a = (idx + 1 == _infiniteDungeon.CurHighestStage) ? 1f : 0f;
        _outline.effectColor = _outlineColor;

        _clearImage.SetActive(idx + 1 < _infiniteDungeon.CurHighestStage);
    }

    private void ClearRewardSlots()
    {
        for (int i = 0; i < _rewardsContainer.childCount; i++)
        {
            _resourceManager.Destroy(_rewardsContainer.GetChild(0).gameObject);
        }
    }
}
