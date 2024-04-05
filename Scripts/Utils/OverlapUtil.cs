using UnityEngine;

public static class OverlapUtil
{
    public static Collider2D[] GetColliderInBox(Vector2 point, Vector2 size, LayerMask layerMask)
    {
        return Physics2D.OverlapBoxAll(point, size, layerMask);
    }

    public static Collider2D[] GetColliderInCircle(Vector2 point, float radius, LayerMask layerMask)
    {
        return Physics2D.OverlapCircleAll(point, radius, layerMask);
    }

    public static bool CheckColliderInBox(Vector2 point, Vector2 size, float angle, LayerMask layerMask)
    {
        var enemy = Physics2D.OverlapBox(point, size, angle, layerMask);

        if (enemy == null) return false;
        return true;
    }
}
