using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_SummonSlot : UI_Base
{
    [SerializeField] private SummonType _type;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _gradeExpText;
    [SerializeField] private Slider _gradeExpSlider;
    [Header("소환 버튼 1")]
    [SerializeField] private Button _summonButton1;
    [SerializeField] private Text _summonCostText1;
    [SerializeField] private Text _summonCountText1;
    [Header("소환 버튼 2")]
    [SerializeField] private Button _summonButton2;
    [SerializeField] private Text _summonCostText2;
    [SerializeField] private Text _summonCountText2;

    private CurrencyManager _currencyManager;
    private SummonSystem _summonSystem;
    private Summon _summon;
    private SummonDataSO _dataSO;

    public override void Init()
    {
        _currencyManager = CurrencyManager.Instance;
        _summonSystem = GameManager.Instance.SummonSystem;
        _summon = _summonSystem.GetSummonInfo(_type);
        _dataSO = _summon.DataSO;
        _summonButton1.onClick.AddListener(() => _summonSystem.SummonWithType(_type, SummonCountType.Small));
        _summonButton2.onClick.AddListener(() => _summonSystem.SummonWithType(_type, SummonCountType.Large));
        _summonSystem.OnSummon += UpdateSummonUI;

        _summonCostText1.text = _dataSO.Cost1.ToString("N0");
        _summonCostText2.text = _dataSO.Cost2.ToString("N0");
        _summonCountText1.text = $"{_dataSO.Count1}회";
        _summonCountText2.text = $"{_dataSO.Count2}회";

        UpdateSummonUI(_type);
    }

    public void UpdateSummonUI(SummonType type)
    {
        if (_type != type) return;
        _levelText.text = $"{_summon.Level}";
        _gradeExpText.text = $"{_summon.CurExp}/{_summon.MaxExp}";
        _gradeExpSlider.maxValue = _summon.MaxExp;
        _gradeExpSlider.value = _summon.CurExp;
        _summonButton1.interactable = (_dataSO.Cost1 <= _currencyManager.GetCurrencyAmount(CurrencyType.Gem));
        _summonButton2.interactable = (_dataSO.Cost2 <= _currencyManager.GetCurrencyAmount(CurrencyType.Gem));
    }
}
