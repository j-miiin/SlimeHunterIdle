using System.Collections.Generic;
using UnityEngine;
using static Enums;
using static Strings.Sprites;
using static Strings.Prefabs;

public class ResourceManager : Singleton<ResourceManager>
{
    // Resources/Sprites 폴더 안의 이미지 소스들 저장
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private Dictionary<GradeType, Sprite> _gradeSprites = new Dictionary<GradeType, Sprite>();

    // Resources/Prefabs 폴더 안의 프리팹들 저장
    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

    public Sprite LoadSprite(string name)
    {
        if (!_sprites.ContainsKey(name))
        {
            // 이미지 소스가 요청 시점에 딕셔너리에 존재하지 않는다면 Load
            Sprite sprite = Resources.Load<Sprite>($"{SPRITES_DATA_PATH}{name}");
            if (!sprite)
            {
#if UNITY_EDITOR
                Debug.LogError($"Sprite 로드 실패 : {name}");
#endif
                return null;
            }
            _sprites.Add(name, sprite);
        }
        return _sprites[name];
    }

    public GameObject Instantiate(string name, Transform parent = null, Transform position = null)
    {
        if (!_prefabs.ContainsKey(name))
        {
            GameObject go = Resources.Load<GameObject>($"{PREFABS_DATA_PATH}{name}");
            if (!go)
            {
#if UNITY_EDITOR
                Debug.LogError($"Prefab 로드 실패 : {name}");
#endif
                return null;
            }
            _prefabs.Add(name, go);
        }

        GameObject obj = _prefabs[name];
        if (obj.TryGetComponent<Poolable>(out Poolable p))
        {
            PoolManager poolManager = PoolManager.Instance;
            if (poolManager)
            {
                return PoolManager.Instance.Pop(obj, name
                    , (position == null) ? obj.transform.position : position.position
                    , obj.transform.rotation
                    , parent).gameObject;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Null Exception : PoolManager");
#endif
                return null;
            }
        }

        return Instantiate(obj
            , (position == null) ? obj.transform.position : position.position
            , obj.transform.rotation
            , parent);
    }

    public GameObject Instantiate(GameObject obj, Transform parent = null, Transform position = null)
    {
        if (obj.TryGetComponent<Poolable>(out Poolable p))
        {
            PoolManager poolManager = PoolManager.Instance;
            if (poolManager)
            {
                return PoolManager.Instance.Pop(obj, name
                    , (position == null) ? obj.transform.position : position.position
                    , obj.transform.rotation
                    , parent).gameObject;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Null Exception : PoolManager");
#endif
                return null;
            }
        }

        return Instantiate(obj
            , (position == null) ? obj.transform.position : position.position
            , obj.transform.rotation
            , parent);
    }

    public void Destroy(GameObject go, float delay = 0f)
    {
        if (go == null) return;

        if (go.TryGetComponent<Poolable>(out Poolable p))
        {
            PoolManager.Instance.Push(p, delay);
            go = null;
            return;
        }
        Object.Destroy(go, delay);
        go = null;
    }

    public Sprite LoadGradeSprite(GradeType type)
    {
        if (!_gradeSprites.ContainsKey(type))
        {
            Sprite sprite = null;
            switch (type)
            {
                case GradeType.D:
                    sprite = Resources.Load<Sprite>($"{SPRITES_DATA_PATH}{GRADE_D_IMAGE}");
                    break;
                case GradeType.C:
                    sprite = Resources.Load<Sprite>($"{SPRITES_DATA_PATH}{GRADE_C_IMAGE}");
                    break;
                case GradeType.B:
                    sprite = Resources.Load<Sprite>($"{SPRITES_DATA_PATH}{GRADE_B_IMAGE}");
                    break;
                case GradeType.A:
                    sprite = Resources.Load<Sprite>($"{SPRITES_DATA_PATH}{GRADE_A_IMAGE}");
                    break;
                case GradeType.S:
                    sprite = Resources.Load<Sprite>($"{SPRITES_DATA_PATH}{GRADE_S_IMAGE}");
                    break;
                case GradeType.SS:
                    sprite = Resources.Load<Sprite>($"{SPRITES_DATA_PATH}{GRADE_SS_IMAGE}");
                    break;
            }
            if (!sprite)
            {
#if UNITY_EDITOR
                Debug.LogError($"Sprite 로드 실패 : {name}");
#endif
                return null;
            }
            _gradeSprites.Add(type, sprite);
        }
        return _gradeSprites[type];
    }
}
