using UnityEngine;


public class GoldDungeonScene : BaseScene
{
    [SerializeField] private GoldDungeonController _controller;

    protected override bool Init()
    {
        if (!base.Init()) return false;
        SceneType = Scenes.GoldDungeonScene;

        DungeonManager.Instance.SetCurrentDungeon(_controller);

        UIManager.GetUIComponent<UI_GameSceneTop>().OpenUI();
        UIManager.GetUIComponent<UI_GameSceneBottom>().OpenUI();
        UIManager.GetUIComponent<UI_GoldDungeonSceneStage>().OpenUI();

        _controller.StartDungeon();

        return true;
    }
}
