using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager
{
    int _order = -20;

    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    
    public UI_Scene SceneUI { get; private set; }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };

            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;      // 캔버스가 어디 있더라도 화면을 전부 덮음.
        canvas.overrideSorting = true;                          // 부모 클래스가 어떤 값을 가지던 내 sorting order 값을 가져야 하기 때문에

        if (sort)
        {
            canvas.sortingOrder = _order;                       // 팝업 UI의 경우 sorting order 값을 _order로 세팅하고
            _order++;                                           // _order 값 증가 => 값이 커질 수록 위에 그려짐.
        }
        else
        {
            canvas.sortingOrder = 0;                            // 고정 UI의 경우 sorting order 값을 0으로 세팅.
        }
    }

    public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/Popup/{name}");

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Utils.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        if (parent != null)
            go.transform.SetParent(parent);
        else if (SceneUI != null)
            go.transform.SetParent(SceneUI.transform);
        else
            go.transform.SetParent(Root.transform);
        
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        // 팝업스택 없으면 종료
        if (_popupStack.Count == 0)
            return;

        // 제일 마지막 스택에 저장된것과 다르면 종료
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }
        
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
            ClosePopupUI();
    }
    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
    }
}
