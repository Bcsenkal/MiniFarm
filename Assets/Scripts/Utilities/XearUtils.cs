using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class XearUtils
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (!component)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }

    
}

public static class TransformExtensions
{
    public static void SetPositionAndScale(this Transform transform, Vector3 targetPos,Vector3 targetScale)
    {
        transform.position = targetPos;
        transform.localScale = targetScale;
    }
    public static void SetChildLayers(this Transform t, int layer)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Transform child = t.GetChild(i);
            child.gameObject.layer = layer;
            SetChildLayers(child, layer);
        }
    }

    public static void LookAtCamera(this Transform t,Camera cam)
    {
        t.LookAt(t.position + cam.transform.forward);
    }
}

public static class Vector3Extensions
{
    public static Vector3 WithReplace(this Vector3 vector3, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector3.x, y ?? vector3.y, z ?? vector3.z);
    }

    public static Vector3 WithIncrement(this Vector3 vector3, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(vector3.x + (x ?? 0), vector3.y + (y ?? 0), vector3.z + (z ?? 0));
    }
}
