using UnityEngine;

public static class NumberUtils
{
    public static float Map(this float value, float from1, float to1, float from2, float to2) {
        return Mathf.Clamp((value - from1) / (to1 - from1) * (to2 - from2) + from2, from2, to2);
    }

    public static Vector3 Lerp(this Vector3 vector3, Vector3 target, float time)
    {
        return new Vector3(
            Mathf.Lerp(vector3.x, target.x, time),
            Mathf.Lerp(vector3.y, target.y, time),
            Mathf.Lerp(vector3.z, target.z, time)
        );
    }
}
