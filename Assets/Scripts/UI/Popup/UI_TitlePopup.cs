using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_TitlePopup : UI_Popup
{
    enum Texts
    {
        TouchToStartText,
        StartButtonText,
        ContinueButtonText,
        CollectionButtonText,
        //DataResetConfirmText
    }

    enum Buttons
    {
        StartButton,
        ContinueButton,
        CollectionButton
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        return true;
    }

    void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");
        Managers.Sound.Play(Sound.Effect, "Sound_FolderItemClick");
        Managers.Game.Init();
    }
}
