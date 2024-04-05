using System;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class StageManager : Singleton<StageManager>
{
    public event Action OnStageUpdate;

    public WaveSystem WaveSystem { get; private set; }
    public Stage Stage { get; private set; }

    #region Stage Level To String
    private readonly Dictionary<StageType, string> _stageLevelToString = new Dictionary<StageType, string>()
    {
        { StageType.Easy, "쉬움" },
        { StageType.Normal, "보통" },
        { StageType.Hard, "어려움" },
        { StageType.VeryHard, "매우 어려움" },
    };
    #endregion

    private Player _player;
    private StageDataHandler _dataHandler;

    public void Init()
    {
        _player = GameManager.Instance.Player;
        _dataHandler = DataManager.Instance.GetDataHandler<StageDataHandler>();
        Stage = _dataHandler.LoadStageInfo();
    }

    public void LoadWaveSystem()
    {
        GameObject obj = ResourceManager.Instance.Instantiate(Strings.Prefabs.WAVE_SYSTEM);
        WaveSystem = obj.GetComponent<WaveSystem>();
        WaveSystem.OnStageClear += GoNextStage;
    }

    public string GetStageLevelString(StageType type)
    {
        return _stageLevelToString[type];
    }

    public void StartWave()
    {
        OnStageUpdate?.Invoke();
        WaveSystem.StartWave();
    }

    private void GoNextStage()
    {
        Stage.LevelUp();
        _dataHandler.SaveStageInfo(Stage);
        Invoke("StartWave", 3f);
    }
}
