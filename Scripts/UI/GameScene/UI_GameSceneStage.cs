using UnityEngine;
using UnityEngine.UI;

public class UI_GameSceneStage : UI_Base
{
    [SerializeField] private Slider _stageValueSlider;
    [SerializeField] private Text _stageText;
    [SerializeField] private Button _bossButton;

    private StageManager _stageManager;
    private Stage _stage;

    private int _stageSliderMaxValue = Numbers.Stage.STAGE_GOAL_COUNT;

    public override void Init()
    {
        base.Init();
        _stageManager = StageManager.Instance;
        _stage = _stageManager.Stage;
        _stageManager.OnStageUpdate += RefreshStageInfoUI;
        _stageManager.WaveSystem.OnMonsterKilled += RefreshStageValueUI;
        _stageValueSlider.maxValue = _stageSliderMaxValue;
    }

    private void RefreshStageInfoUI()
    {
        _stageText.text = $"{_stageManager.GetStageLevelString(_stage.Type)} {_stage.MainLevel}-{_stage.SubLevel}";
        _stageValueSlider.value = 0;
    }

    public void RefreshStageValueUI(int curStageValue)
    {
        _stageValueSlider.value = curStageValue;
    }
}
