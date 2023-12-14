using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType = Define.Scene.Unknown;

    protected bool _init = false;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)                                                                      // 이미 생성되어 있으면 생성하지 않음.
            return false;

        _init = true;
        GameObject go = GameObject.Find("EventSystem");
        if (go == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";  // EventSystem이 없다면 생성함.

        return true;
    }

    public virtual void Clear(){}
}
