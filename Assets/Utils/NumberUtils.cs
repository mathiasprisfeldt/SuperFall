using UnityEngine;

public static class NumberUtils
{
    public static float Map(this float value, float from1, float to1, float from2, float to2) {
        return Mathf.Clamp((value - from1) / (to1 - from1) * (to2 - from2) + from2, from2, to2);
    }
}
