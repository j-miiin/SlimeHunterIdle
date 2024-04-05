using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float _cameraSpeed = 3f;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector2 _mapSize;
    [SerializeField] private Vector2 _center;

    private float _height;
    private float _width;

    private void Start()
    {
        _height = Camera.main.orthographicSize;
        _width = _height * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        if (_target == null) _target = GameManager.Instance.Player.transform;

        _targetPosition.Set(_target.position.x, _target.position.y, this.transform.position.z);

        // vectorA -> B까지 T의 속도로 이동
        this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, _cameraSpeed * Time.deltaTime);

        float lx = _mapSize.x - _width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + _center.x, lx + _center.x);

        float ly = _mapSize.y - _height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + _center.y, ly + _center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    public void ChangeTarget(Transform target)
    {
        _target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_center, _mapSize * 2);
    }
}
