using UnityEngine;

#if UNITY_EDITOR
public static class DebugUtil
{
    public static void Assert(bool condition, string name)
    {
        Debug.Assert(condition, $"Null Exception : {name}");
    }
}
#endif
