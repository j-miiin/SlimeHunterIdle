using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Enums;
using static Strings.Dungeon;

public class UI_GoldDungeonSceneStage : UI_Base
{
    [SerializeField] private Slider _stageValueSlider;
    [SerializeField] private Text _stageText;
    [SerializeField] private Text _killedMonsterText;
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Text _timeText;
    [SerializeField] private Button _exitButton;

    private DungeonManager _dungeonManager;
    private UIManager _uiManager;
    private GoldDungeonController _controller;
    private GoldDungeon _dungeon;

    private bool _isExitButtonClicked;
    private Coroutine _exitCoroutine;

    public override void Init()
    {
        base.Init();
        _dungeonManager = DungeonManager.Instance;
        _uiManager = UIManager.Instance;
        _controller = _dungeonManager.CurController as GoldDungeonController;
        _dungeon = _dungeonManager.GetDungeon(DungeonType.Gold) as GoldDungeon;

        _controller.OnKilledMonsterCountUpdate += UpdateMonsterCount;
        _controller.OnStageUpdate += UpdateStage;
        _controller.OnTimeUpdate += UpdateTime;

        _stageValueSlider.maxValue = _controller.MonsterPerStageCount;
        _timeSlider.maxValue = _dungeon.DataSO.Time;
        _exitButton.onClick.AddListener(OnClickExitButton);

        UpdateStage(1);
        UpdateMonsterCount(0, 0);
        UpdateTime(_dungeon.DataSO.Time);
    }

    public void UpdateTime(float time)
    {
        _timeSlider.value = time;
        _timeText.text = time.ToString("N2");
    }

    private void UpdateMonsterCount(int sliderValue, int elapsedMonsterCount)
    {
        _stageValueSlider.value = sliderValue;
        _killedMonsterText.text = $"{elapsedMonsterCount}{GOLD_DUNGEON_MONSTER_COUNT_STR}";
    }

    private void UpdateStage(int stage)
    {
        _stageValueSlider.value = 0;
        _stageText.text = $"{stage}{GOLD_DUNGEON_STAGE_STR}";
    }

    private void OnClickExitButton()
    {
        if (_isExitButtonClicked)
        {
            if (_exitCoroutine != null) StopCoroutine(_exitCoroutine);
            SceneManagerEx.Instance.LoadScene(Scenes.GameScene);
        }
        else
        {
            _isExitButtonClicked = true;
            _uiManager.ShowPopup<UI_NotifyPopup>(new BasePopupParameter(content: Strings.Popup.NOTIFY_EXIT_POPUP_CONTENT));
            _exitCoroutine = StartCoroutine(COExitTimer());
        }
    }

    private IEnumerator COExitTimer()
    {
        float elapsedTime = 0f;
        while (elapsedTime <= 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _isExitButtonClicked = false;
    }
}
