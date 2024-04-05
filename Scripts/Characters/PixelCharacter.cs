using UnityEngine;

public class PixelCharacter : MonoBehaviour
{
    public PixelCharacterController Controller { get; private set; }
    public HealthSystem HealthSystem { get; private set; }
    public Animator Animator { get; private set; }

    //private Dictionary<string, float> _animationLengthDic = new Dictionary<string, float>();

    protected virtual void Awake()
    {
        Controller = GetComponent<PixelCharacterController>();
        HealthSystem = GetComponent<HealthSystem>();
        Animator = GetComponentInChildren<Animator>();
        //InitializeAnimationLengths();
    }

    //public float GetAnimationLength(string animationName)
    //{
    //    if (_animationLengthDic.TryGetValue(animationName, out float length))
    //    {
    //        return length;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Animation not found: " + animationName);
    //        return 0f;
    //    }
    //}

    //private void InitializeAnimationLengths()
    //{
    //    AnimationClip[] clips = Animator.runtimeAnimatorController.animationClips;
    //    foreach (AnimationClip clip in clips)
    //    {
    //        _animationLengthDic[clip.name] = clip.length;
    //    }
    //}
}
