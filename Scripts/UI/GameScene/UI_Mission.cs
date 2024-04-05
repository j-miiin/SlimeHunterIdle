using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_Mission : UI_Base
{
    [SerializeField] private Text _missionDescriptionText;
    [SerializeField] private Text _missionValueText;
    [SerializeField] private Image _rewardIconImage;
    [SerializeField] private Text _rewardValueText;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Button _completeButton;

    private QuestManager _questManager;
    private RepeatQuest _curQuest;

    private Color _originalColor;
    private Color _completeColor;

    public override void Init()
    {
        base.Init();
        _questManager = QuestManager.Instance;
        _curQuest = _questManager.GetCurrentRepeatQuest();
        UpdateQuestInfo();
        _questManager.OnQuestValueUpdate += UpdateQuestValue;
        _curQuest.OnQuestAchieve += UpdateCompleteState;
        _completeButton.onClick.AddListener(CompleteQuest);
        _originalColor = _backgroundImage.color;
        _completeColor = Colors.MainYellow;
        _completeColor.a = _originalColor.a;

        UpdateQuestValue(_curQuest.DataSO.QuestTaskType);
        if (_curQuest.IsCompleted) UpdateCompleteState();
    }

    public override void Clear()
    {
        base.Clear();
        _questManager.OnQuestValueUpdate -= UpdateQuestValue;
        _curQuest.OnQuestAchieve -= UpdateCompleteState;
    }

    private void UpdateQuestInfo()
    {
        QuestDataSO data = _curQuest.DataSO;
        _missionDescriptionText.text = _questManager.GetQuestDescription(data.QuestTaskType, data.RequiredQuestValue);
        _missionValueText.text = $"{_curQuest.CurQuestValue}/{data.RequiredQuestValue}";
        _rewardValueText.text = $"x{data.RewardValue}";
    }

    private void UpdateQuestValue(QuestTaskType type)
    {
        if (type != _curQuest.DataSO.QuestTaskType) return;
        int curValue = (_curQuest.IsCompleted) ? _curQuest.DataSO.RequiredQuestValue : _curQuest.CurQuestValue;
        _missionValueText.text = $"{curValue}/{_curQuest.DataSO.RequiredQuestValue}";
    }

    private void UpdateCompleteState()
    {
        _backgroundImage.DOColor(_completeColor, 0.8f).SetLoops(-1, LoopType.Yoyo);
    }

    private void CompleteQuest()
    {
        if (!_curQuest.IsCompleted) return;
        _curQuest.CompleteQuest();
        _curQuest = _questManager.GetCurrentRepeatQuest();
        UpdateQuestInfo();
        _backgroundImage.DOKill();
        _backgroundImage.color = _originalColor;
    }
}
