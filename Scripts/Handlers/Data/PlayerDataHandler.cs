using static Enums;

public struct PlayerProfileData
{
    public string nickName;
    public int level;
    public int curExp;
    public int maxExp;

    public PlayerProfileData(string nickName, int level, int curExp, int maxExp)
    {
        this.nickName = nickName;
        this.level = level;
        this.curExp = curExp;
        this.maxExp = maxExp;
    }
}

public class PlayerDataHandler : DataHandler
{
    // 프로필 및 레벨 관련 정보
    public void SaveProfile(Player player)
    {
        ES3.Save<string>("nickname", player.NickName);
        ES3.Save<int>("level", player.Level);
        ES3.Save<int>("curExp", player.CurExp);
        ES3.Save<int>("maxExp", player.MaxExp);
    }

    public void SaveLevelStatus(Player player, PlayerLevelStatusType type)
    {
        switch (type)
        {
            case PlayerLevelStatusType.Level:
                ES3.Save<int>("level", player.Level);
                break;
            case PlayerLevelStatusType.CurExp:
                ES3.Save<int>("curExp", player.CurExp);
                break;
            case PlayerLevelStatusType.MaxExp:
                ES3.Save<int>("maxExp", player.MaxExp);
                break;
        }
    }

    // 프로필 및 레벨 관련 정보
    public PlayerProfileData LoadProfile()
    {
        PlayerProfileData data = new PlayerProfileData();
        if (ES3.KeyExists("nickname")) data.nickName = ES3.Load<string>("nickname");
        else data.nickName = "애옹";

        if (ES3.KeyExists("level")) data.level = ES3.Load<int>("level");
        else data.level = 1;

        if (ES3.KeyExists("curExp")) data.curExp = ES3.Load<int>("curExp");
        else data.curExp = 0;

        if (ES3.KeyExists("maxExp")) data.maxExp = ES3.Load<int>("maxExp");
        else data.maxExp = 3000;

        return data;
    }
}
