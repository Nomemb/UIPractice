using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
}
