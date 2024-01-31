using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IntroPopup : UI_Popup
{
    public enum GameObjects
    {
        Intro1,
        Intro2,
        Intro3,
        Guide1,
        Guide2,
        Guide3,
        
    }

    enum Texts
    {
        IntroText
    }

    Action _onEndCallback;
    int _selectedIndex;
    int _startIndex = (int)GameObjects.Intro1;
    int _lastIndex = (int)GameObjects.Guide3;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        
        gameObject.BindEvent(OnClickImage);

        _selectedIndex = _startIndex;
        
        RefreshUI();
        return true;
    }

    public void SetInfo(int startIndex, int endIndex, Action onEndCallback)
    {
        _onEndCallback = onEndCallback;
        _selectedIndex = startIndex;
        _startIndex = startIndex;
        _lastIndex = endIndex;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (_init == false)
            return;
        
        GetObject((int)GameObjects.Intro1).SetActive(false);
        GetObject((int)GameObjects.Intro2).SetActive(false);
        GetObject((int)GameObjects.Intro3).SetActive(false);
        GetObject((int)GameObjects.Guide1).SetActive(false);
        GetObject((int)GameObjects.Guide2).SetActive(false);
        GetObject((int)GameObjects.Guide3).SetActive(false);
        
        // 정상 범위 안에 들어오면 해당 오브젝트 활성화
        if(_selectedIndex <= (int)GameObjects.Guide3)
            GetObject(_selectedIndex).SetActive(true);

        switch (_selectedIndex)
        {
            case (int)GameObjects.Intro1:
                GetText((int)Texts.IntroText).text = Managers.GetText(Define.Intro1);
                break;
            
            case (int)GameObjects.Intro2:
                GetText((int)Texts.IntroText).text = Managers.GetText(Define.Intro2);
                break;

            case (int)GameObjects.Intro3:
                GetText((int)Texts.IntroText).text = Managers.GetText(Define.Intro3);
                break;
            
            default:
                GetText((int)Texts.IntroText).text = "";
                break;
        }
    }

    /// <summary>
    /// 바인딩 해 놓은 범위 안에 들어갈 시 다음 이미지를 활성화함.
    /// 전부 활성화했을 경우 해당 UI를 닫음
    /// </summary>
    private void OnClickImage()
    {
        Debug.Log("OnClickImage");

        // 끝났으면 해당 화면 닫음
        if (_selectedIndex == (int)GameObjects.Guide3)
        {
            Managers.UI.ClosePopupUI(this);
            _onEndCallback?.Invoke();
            return;
        }

        // 다음 화면으로 이동
        _selectedIndex++;
        RefreshUI();
    }
}
