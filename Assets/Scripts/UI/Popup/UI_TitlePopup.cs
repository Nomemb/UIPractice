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
        GetButton((int)Buttons.ContinueButton).gameObject.BindEvent(OnClickContinueButton);
        GetButton((int)Buttons.CollectionButton).gameObject.BindEvent(OnClickCollectionButton);

        GetText((int)Texts.StartButtonText).text = Managers.GetText(Define.StartButtonText);
        GetText((int)Texts.ContinueButtonText).text = Managers.GetText(Define.ContinueButtonText);
        GetText((int)Texts.CollectionButtonText).text = Managers.GetText(Define.CollectionButtonText);

        return true;
    }

    void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");
        Managers.Sound.Play(Sound.Effect, "Sound_FolderItemClick");
        Managers.Game.Init();

        // 데이터가 있는 경우
        if (Managers.Game.LoadGame())
        {
            Managers.UI.ShowPopupUI<UI_ConfirmPopup>().SetInfo(() =>
            {
                Managers.Game.Init();
                Managers.Game.SaveGame();
                
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UI_NamePopup>();
            }, Managers.GetText(Define.DataResetConfirm));
        }
        else
        {
            Managers.Game.Init();
            Managers.Game.SaveGame();
            
            Managers.UI.ClosePopupUI(this);
            Managers.UI.ShowPopupUI<UI_NamePopup>();
        }
    }

    void OnClickContinueButton()
    {
        Debug.Log("OnClickContinueButton");
        Managers.Sound.Play(Sound.Effect, "Sound_FolderItemClick");
        Managers.Game.Init();
        Managers.Game.LoadGame();
        
        Managers.UI.ClosePopupUI(this);
        // Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
    
    void OnClickCollectionButton()
    {
        Debug.Log("OnClickCollectionButton");
        Managers.Sound.Play(Sound.Effect, "Sound_FolderItemClick");
        Managers.Game.Init();
        Managers.Game.LoadGame();
        
        // Managers.UI.ShowPopupUI<UI_CollectionPopup>();
    }
}
