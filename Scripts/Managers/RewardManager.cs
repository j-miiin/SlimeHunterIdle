using Keiwando.BigInteger;
using static Enums;

public class RewardManager : Singleton<RewardManager>
{
    private CurrencyManager _currencyManager;
    private Player _player;

    public void Init()
    {
        _currencyManager = CurrencyManager.Instance;
        _player = GameManager.Instance.Player;
    }

    public void GiveReward(RewardType type, BigInteger value)
    {
        switch (type)
        {
            case RewardType.Gold:
                _currencyManager.AddCurrency(CurrencyType.Gold, value); break;
            case RewardType.Gem:
                _currencyManager.AddCurrency(CurrencyType.Gem, value); break;
            default:
                break;
        }
    }
}
