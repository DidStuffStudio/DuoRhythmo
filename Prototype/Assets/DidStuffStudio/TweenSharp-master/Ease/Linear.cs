﻿using TS;

public static class Linear
{
    public static TSEase.EaseFunction EaseNone = FEase;
    public static TSEase.EaseFunction EaseIn = FEase;
    public static TSEase.EaseFunction EaseOut = FEase;
    public static TSEase.EaseFunction EaseInOut = FEase;

    private static float FEase(float t, float b, float c, float d, object p = null)
    {
        return c * t / d + b;
    }
}
