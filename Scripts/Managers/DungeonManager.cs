using System.Collections.Generic;
using static Enums;

public class DungeonManager : Singleton<DungeonManager>
{
    public DungeonController CurController { get; private set; }

    private Dictionary<DungeonType, Dungeon> _dungeonDic;
    private DungeonDataHandler _dataHandler;
    private SceneManagerEx _sceneManagerEx;

    public void Init()
    {
        _dataHandler = DataManager.Instance.GetDataHandler<DungeonDataHandler>();
        _dungeonDic = _dataHandler.LoadDungeons();
    }

    public Dungeon GetDungeon(DungeonType type)
    {
        if (!_dungeonDic.ContainsKey(type)) return null;
        return _dungeonDic[type];
    }

    public void SetCurrentDungeon(DungeonController controller)
    {
        CurController = controller;
    }

    public void EnterDungeon(DungeonType type)
    {
        if (!_sceneManagerEx) _sceneManagerEx = SceneManagerEx.Instance;
        switch (type)
        {
            case DungeonType.Gold:
                _sceneManagerEx.LoadScene(Scenes.GoldDungeonScene);
                break;
        }
    }

    public void ClearDungeon(DungeonType type)
    {

    }
}
