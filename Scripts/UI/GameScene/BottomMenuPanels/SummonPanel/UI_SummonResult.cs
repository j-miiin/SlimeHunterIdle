using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static Enums;
using static Strings.Prefabs;

public class UI_SummonResult : UI_Base
{
    [SerializeField] private RectTransform _slotContainer;
    [SerializeField] private Button _closeButton;
    [Header("소환 버튼 1")]
    [SerializeField] private Button _summonButton1;
    [SerializeField] private Text _summonCostText1;
    [SerializeField] private Text _summonCountText1;
    [Header("소환 버튼 2")]
    [SerializeField] private Button _summonButton2;
    [SerializeField] private Text _summonCostText2;
    [SerializeField] private Text _summonCountText2;

    private SummonSystem _summonSystem;
    private CurrencyManager _currencyManager;
    private ResourceManager _resourceManager;

    private UI_SummonResultSlot[] _slotList;

    private bool _isInit;
    private string _resultSlotPath = UI_SUMMON_RESULT_SLOT_PATH;
    private WaitForSeconds _slotAnimationDelay = new WaitForSeconds(0.005f);

    private void Init()
    {
        _isInit = true;
        _summonSystem = GameManager.Instance.SummonSystem;
        _currencyManager = CurrencyManager.Instance;
        _resourceManager = ResourceManager.Instance;
        _closeButton.onClick.AddListener(CloseUI);
    }

    // 장비 소환 결과
    public void RefreshSummonResultUI(List<Equipment> resultList)
    {
        if (!_isInit) Init();
        SummonType type = SummonType.Equipment;
        Summon summon = _summonSystem.GetSummonInfo(type);
        SummonDataSO dataSO = summon.DataSO;

        RefreshSummonResultSlots(resultList);
        RefreshButtons(type, dataSO.Cost1, dataSO.Cost2, dataSO.Count1, dataSO.Count2);
    }

    // 스킬 소환 결과
    public void RefreshSummonResultUI(List<Skill> resultList)
    {
        if (!_isInit) Init();
        SummonType type = SummonType.Skill;
        Summon summon = _summonSystem.GetSummonInfo(type);
        SummonDataSO dataSO = summon.DataSO;

        RefreshSummonResultSlots(resultList);
        RefreshButtons(type, dataSO.Cost1, dataSO.Cost2, dataSO.Count1, dataSO.Count2);
    }

    // 장비 소환 결과
    private void RefreshSummonResultSlots(List<Equipment> resultList)
    {
        StopAllCoroutines();
        DOTween.KillAll();
        DestroySlots(_slotContainer.childCount, 0);
        Dictionary<Equipment, int> resultDic = new Dictionary<Equipment, int>();
        for (int i = 0; i < resultList.Count; i++)
        {
            Equipment key = resultList[i];
            if (resultDic.ContainsKey(key)) resultDic[key]++;
            else resultDic.Add(key, 1);
        }

        List<Equipment> resultSet = resultDic.Keys.ToList();
        int dataCnt = resultSet.Count;
        int slotCnt = _slotContainer.childCount;
        CreateSlots(slotCnt, dataCnt);

        _slotList = new UI_SummonResultSlot[dataCnt];
        for (int i = 0; i < dataCnt; i++)
        {
            GameObject slotObj = _slotContainer.GetChild(i).gameObject;
            _slotList[i] = slotObj.GetComponent<UI_SummonResultSlot>();
            _slotList[i].OpenUI();
            _slotList[i].SetResultSlot(
                resultSet[i].DataSO.EquipmentIcon
                , _resourceManager.LoadGradeSprite(resultSet[i].DataSO.Grade)
                , resultDic[resultSet[i]]
                , resultSet[i].DataSO.Grade
                );
        }

        StartCoroutine(COShowSlotWithAnimation(dataCnt));
        DestroySlots(slotCnt, dataCnt);
    }

    // 스킬 소환 결과
    private void RefreshSummonResultSlots(List<Skill> resultList)
    {
        StopAllCoroutines();
        DOTween.KillAll();
        DestroySlots(_slotContainer.childCount, 0);
        Dictionary<Skill, int> resultDic = new Dictionary<Skill, int>();
        for (int i = 0; i < resultList.Count; i++)
        {
            Skill key = resultList[i];
            if (resultDic.ContainsKey(key)) resultDic[key]++;
            else resultDic.Add(key, 1);
        }

        List<Skill> resultSet = resultDic.Keys.ToList();
        int dataCnt = resultSet.Count;
        int slotCnt = _slotContainer.childCount;
        CreateSlots(slotCnt, dataCnt);

        _slotList = new UI_SummonResultSlot[dataCnt];
        for (int i = 0; i < dataCnt; i++)
        {
            GameObject slotObj = _slotContainer.GetChild(i).gameObject;
            _slotList[i] = slotObj.GetComponent<UI_SummonResultSlot>();
            _slotList[i].OpenUI();
            _slotList[i].SetResultSlot(
                resultSet[i].DataSO.SkillIcon
                , _resourceManager.LoadGradeSprite(resultSet[i].DataSO.Grade)
                , resultDic[resultSet[i]]
                , resultSet[i].DataSO.Grade
                );
        }

        StartCoroutine(COShowSlotWithAnimation(dataCnt));
        DestroySlots(slotCnt, dataCnt);
    }

    // 버튼 초기화
    private void RefreshButtons(SummonType type, int cost1, int cost2, int count1, int count2)
    {
        _summonButton1.onClick.RemoveAllListeners();
        _summonButton2.onClick.RemoveAllListeners();
        _summonButton1.onClick.AddListener(() => _summonSystem.SummonWithType(type, SummonCountType.Small));
        _summonButton2.onClick.AddListener(() => _summonSystem.SummonWithType(type, SummonCountType.Large));

        _summonCostText1.text = cost1.ToString("N0");
        _summonCostText2.text = cost2.ToString("N0");
        _summonCountText1.text = $"{count1}회";
        _summonCountText2.text = $"{count2}회";

        _summonButton1.interactable = (cost1 <= _currencyManager.GetCurrencyAmount(CurrencyType.Gem));
        _summonButton2.interactable = (cost2 <= _currencyManager.GetCurrencyAmount(CurrencyType.Gem));
    }

    // 슬롯 생성
    private void CreateSlots(int slotCnt, int dataCnt)
    {
        // 슬롯의 개수가 데이터 개수보다 적을 경우
        while (slotCnt < dataCnt)
        {
            GameObject go = ResourceManager.Instance.Instantiate(_resultSlotPath, parent: _slotContainer);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            slotCnt = _slotContainer.childCount;
        }
    }

    // 슬롯 삭제
    private void DestroySlots(int slotCnt, int dataCnt)
    {
        for (int i = 0; i < (slotCnt - dataCnt); i++)
        {
            GameObject go = _slotContainer.GetChild(dataCnt).gameObject;
            ResourceManager.Instance.Destroy(go);
        }
    }

    private IEnumerator COShowSlotWithAnimation(int dataCnt)
    {
        for (int i = 0; i < dataCnt; i++)
        {
            _slotList[i].OpenUI(UIAnimationType.Scale, false);
            yield return _slotAnimationDelay;
        }
    }
}
