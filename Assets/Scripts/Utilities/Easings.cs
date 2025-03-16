using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Easings 
{
    public static float QuadEaseOut(float t, float b, float c, float d)
    {
        return -c * (t /= d) * (t - 2) + b;
    }
    
    public static float QuadEaseIn(float t, float b, float c, float d)
    {
        return c * (t /= d) * t + b;
    }
    
    public static float QuadEaseInOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
            return c / 2 * t * t + b;

        return -c / 2 * ((--t) * (t - 2) - 1) + b;
    }
    
    public static float ExpoEaseOut(float t, float b, float c, float d)
    {
        return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
    }
    
    public static float ExpoEaseIn(float t, float b, float c, float d)
    {
        return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
    }
    
    public static float BackEaseOut(float t, float b, float c, float d)
    {
        return c * ((t = t / d - 1) * t * ((1.70158f + 1) * t + 1.70158f) + 1) + b;
    }
    
    public static float BackEaseIn(float t, float b, float c, float d)
    {
        return c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
    }
    
    ////////
    public static float EaseOutBack(float x, float s)
    {
        return 1 + ((1.70158f*s) + 1) * Mathf.Pow(x - 1, 3) + (1.70158f*s) * Mathf.Pow(x - 1, 2);
    }
    public static float easeOutBack(float start, float end, float val, float overshoot = 1.5f){
        float s = 1.70158f * overshoot;
        end -= start;
        val = (val / 1) - 1;
        return end * ((val) * val * ((s + 1) * val + s) + 1) + start;
    }
    
    public static float easeInBack(float start, float end, float val, float overshoot = 1.5f)
    {
        end -= start;
        val /= 1;
        float s = 1.70158f * overshoot;
        return end * (val) * val * ((s + 1) * val - s) + start;
    }
    
    public static float easeInQuart(float start, float end, float val){
        end -= start;
        return end * val * val * val * val + start;
    }
    
    
    
}
