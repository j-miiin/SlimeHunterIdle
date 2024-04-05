using System;
using UnityEngine;
using static Enums;
using Random = UnityEngine.Random;

public class WaveSystem : MonoBehaviour
{
    public event Action<int> OnMonsterKilled;
    public event Action OnStageClear;

    public int KillMonsterCount { get; protected set; }

    [SerializeField] private GameObject[] _spawnMonsterList;
    [SerializeField] private GameObject _bossMonster;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _monsterSpawner;
    [SerializeField] private int _goalCount;
    [SerializeField] protected int MaxSpawnCount;

    protected bool IsWaveEnd;
    protected int AliveMonsterCount;
    private bool _isInit = false;

    private QuestManager _questManager;
    private Player _player;

    private void Init()
    {
        _isInit = true;
        _questManager = QuestManager.Instance;
        _player = GameManager.Instance.Player;
        //_spawnMonsterList = Resources.LoadAll<GameObject>(Strings.Prefabs.MONSTER_FILE_PATH);
        //_bossMonster = Resources.Load<GameObject>(Strings.Prefabs.BOSS_MONSTER_FILE_PATH);
    }

    protected virtual void Update()
    {
        if (KillMonsterCount >= _goalCount && !IsWaveEnd)
        {
            ClearMonsters();
            _player.PlayerController.ChangeTargetingState(false);
            Invoke("SpawnBossMonster", 2f);
            Invoke("StartTargetingBoss", 3f);
        }

        if (!IsWaveEnd && AliveMonsterCount < MaxSpawnCount)
        {
            AliveMonsterCount++;
            SpawnMonster();
        }
    }

    public void StartWave()
    {
        if (!_isInit) Init();
        KillMonsterCount = 0;
        AliveMonsterCount = MaxSpawnCount;
        IsWaveEnd = false;
        for (int i = 0; i < MaxSpawnCount; i++) SpawnMonster();
    }

    public void SpawnMonster()
    {
        int rndSpawnMonsterIdx = Random.Range(0, _spawnMonsterList.Length);
        int rndSpawnPointIdx = Random.Range(0, _spawnPoints.Length);
        GameObject obj = ResourceManager.Instance.Instantiate(
            _spawnMonsterList[rndSpawnMonsterIdx]
            , parent: _monsterSpawner
            , position: _spawnPoints[rndSpawnPointIdx]
            );
        if (obj.TryGetComponent(out Monster monster))
        {
            monster.Init(new CharacterStat(10, 500));
            monster.HealthSystem.OnDeath += () => {
                KillMonsterCount++;
                AliveMonsterCount--;
                _player.EarnExp(50);
                _questManager.UpdateQuest(QuestTaskType.KillMonster, 1);
                OnMonsterKilled?.Invoke(KillMonsterCount);
            };
        }
    }

    public void SpawnBossMonster()
    {
        int rndSpawnPointIdx = Random.Range(0, _spawnPoints.Length);
        GameObject obj = ResourceManager.Instance.Instantiate(_bossMonster, position: _spawnPoints[rndSpawnPointIdx]);
        if (obj.TryGetComponent(out Monster monster))
        {
            monster.Init(new CharacterStat(50, 1000));
            monster.HealthSystem.OnDamage += () => {
                // UI Update Event
            };
            monster.HealthSystem.OnDeath += () =>
            {
                _questManager.UpdateQuest(QuestTaskType.KillMonster, 1);
                OnStageClear?.Invoke();
            };
        }
    }

    public void ClearMonsters()
    {
        IsWaveEnd = true;
        for (int i = 0; i < _monsterSpawner.childCount; i++)
        {
            Monster monster = _monsterSpawner.GetChild(i).GetComponent<Monster>();
            if (monster.HealthSystem.IsDead) continue;
            monster.HealthSystem.CallOnDeath();
        }
    }

    private void StartTargetingBoss()
    {
        _player.PlayerController.ChangeTargetingState(true);
    }
}
