using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_NamePopup : UI_Popup
{
    enum GameObjects
    {
        InputField
    }

    enum Texts
    {
        ConfirmButtonText,
        NameText,
        HintText,
        ValueText
    }

    enum Buttons
    {
        ConfirmButton
    }

    private TMP_InputField _inputField;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.ConfirmButton).gameObject.BindEvent(OnclickConfirmButton);

        _inputField = GetObject((int)GameObjects.InputField).gameObject.GetComponent<TMP_InputField>();
        _inputField.text = "";

        RefreshUI();

        return true;
    }

    private void RefreshUI()
    {
        GetText((int)Texts.NameText).text = Managers.GetText(Define.Sinibe);
    }
    private void OnclickConfirmButton()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Sound_Checkbutton");
        
        Debug.Log("OnclickConfirmButton");
        Debug.Log($"Input Id {_inputField.text}");

        Managers.Game.Name = _inputField.text;
        
        Managers.UI.ClosePopupUI(this);

        Managers.UI.ShowPopupUI<UI_IntroPopup>().SetInfo((int)UI_IntroPopup.GameObjects.Intro1,
            (int)UI_IntroPopup.GameObjects.Intro3,
            () =>
            {
                Managers.UI.ShowPopupUI<UI_PlayPopup>();
            });

    }
}
