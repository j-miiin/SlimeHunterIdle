using UnityEngine;

public class MonsterAppearState : MonsterBaseState
{
    private float _appearTime = 2f;
    private float _elapsedTime = 0f;
    private GameManager _gameManager;
    private CameraController _cameraController;

    public MonsterAppearState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
        _gameManager = GameManager.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        _cameraController = _gameManager.MainCamera.GetComponent<CameraController>();
        _elapsedTime = 0f;
        StartAnimation(MonsterStateMachine.Monster.AnimationData.AppearParameterHash);
        _cameraController.ChangeTarget(MonsterStateMachine.Monster.transform);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_elapsedTime < _appearTime)
        {
            _elapsedTime += Time.deltaTime;
        } else
        {
            OnIdle();
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(MonsterStateMachine.Monster.AnimationData.AppearParameterHash);
        MonsterController.TargetPlayer();
        _cameraController.ChangeTarget(_gameManager.Player.transform);
    }
}
