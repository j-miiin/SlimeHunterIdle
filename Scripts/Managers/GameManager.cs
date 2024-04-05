using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Camera MainCamera { get; private set; }
    public Player Player { get; private set; }
    public SummonSystem SummonSystem { get; private set; }
    public Transform[] PlayerSpawnPosList { get; private set; }

    private bool _isInit;
    private ResourceManager _resourceManager;

    public void Init(Transform[] _playerSpawnPosList)
    {
        if (_isInit) return;
        _isInit = true;
        MainCamera = Camera.main;
        PlayerSpawnPosList = _playerSpawnPosList;
        _resourceManager = ResourceManager.Instance;
        SpawnPlayer();
        CurrencyManager.Instance.Init();
        StageManager.Instance.Init();
        SkillManager.Instance.Init();
        QuestManager.Instance.Init();
        RewardManager.Instance.Init();
        DungeonManager.Instance.Init();
        SummonSystem = new SummonSystem();
    }

    public void SpawnPlayer()
    {
        int posIdx = Random.Range(0, PlayerSpawnPosList.Length);
        if (!Player)
        {
            GameObject obj = _resourceManager.Instantiate(Strings.Prefabs.PLAYER, position: PlayerSpawnPosList[posIdx]);
            if (obj) Player = obj.GetComponent<Player>();
            DontDestroyOnLoad(Player);
            Player.Init();
        }
        else
        {
            Player.transform.position = PlayerSpawnPosList[posIdx].position;
        }
    }
}
