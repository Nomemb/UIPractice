using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action action, Define.UIEvent type = Define.UIEvent.Click) // 확장 메서드. 다른 GameObject에서도 BindEvent를 사용 가능.
    {
        UI_Base.BindEvent(go, action, type);
    }
}