using Keiwando.BigInteger;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_GameSceneTop : UI_Base
{
    #region Serialize Fields
    [Header("Player Info")]
    [SerializeField] private Image _playerIconImage;
    [SerializeField] private Text _nicknameText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _expText;
    [SerializeField] private Slider _expSlider;

    [Header("Currency Info")]
    [SerializeField] private Text _goldValueText;
    [SerializeField] private Text _gemValueText;
    #endregion

    private GameManager _gameManager;
    private CurrencyManager _currencyManager;
    private Player _player;

    public override void Init()
    {
        base.Init();
        _gameManager = GameManager.Instance;
        _currencyManager = CurrencyManager.Instance;
        _player = _gameManager.Player;

        // Add Event
        _player.OnLevelUpdate += RefreshLevelUI;
        _currencyManager.OnCurrencyUpdate += RefreshCurrencyUI;

        // Refresh UI
        RefreshProfileUI();
        RefreshLevelUI();
        RefreshCurrencyUI(CurrencyType.Gold);
        RefreshCurrencyUI(CurrencyType.Gem);
    }

    public override void Clear()
    {
        base.Clear();
        _player.OnLevelUpdate -= RefreshLevelUI;
        _currencyManager.OnCurrencyUpdate -= RefreshCurrencyUI;
    }

    // 프로필
    private void RefreshProfileUI()
    {
        Debug.Log("닉네임:" + _player.NickName);

        _nicknameText.text = _player.NickName;
    }

    // 레벨 및 경험치
    private void RefreshLevelUI()
    {
        _levelText.text = $"Lv.{_player.Level}";
        _expText.text = $"EXP {((float)_player.CurExp / _player.MaxExp * 100).ToString("N2")}%";
        _expSlider.maxValue = _player.MaxExp;
        _expSlider.value = _player.CurExp;
    }

    // 재화
    private void RefreshCurrencyUI(CurrencyType currencyType)
    {
        BigInteger currency = _currencyManager.GetCurrencyAmount(currencyType);
        switch (currencyType)
        {
            case CurrencyType.Gold:
                _goldValueText.text = currency.ChangeMoney();
                break;
            case CurrencyType.Gem:
                _gemValueText.text = currency.ChangeMoney();
                break;
        }
    }
}
