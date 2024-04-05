using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] private Transform[] _playerSpawnPosList;
    
    protected override bool Init()
    {
        if (!base.Init()) return false;
        SceneType = Scenes.GameScene;

        GameManager.Instance.Init(_playerSpawnPosList);
        StageManager.Instance.LoadWaveSystem();

        UIManager.GetUIComponent<UI_GameSceneTop>().OpenUI();
        UIManager.GetUIComponent<UI_GameSceneBottom>().OpenUI();
        UIManager.GetUIComponent<UI_GameSceneStage>().OpenUI();
        UIManager.GetUIComponent<UI_Mission>().OpenUI();
        UIManager.GetUIComponent<UI_BottomMenuBar>().OpenUI();

        StageManager.Instance.StartWave();

        return true;
    }
}
