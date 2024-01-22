using System;

public class UI_ConfirmPopup : UI_Popup
{
    enum Texts
    {
        MessageText
    }

    enum Buttons
    {
        YesButton,
        NoButton
    }

    private string _text;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.YesButton).gameObject.BindEvent(OnClickYesButton);
        GetButton((int)Buttons.NoButton).gameObject.BindEvent(OnClickNoButton);

        GetText((int)Texts.MessageText).text = _text;

        RefreshUI();
        return true;
    }

    private Action _onClickYesButton;

    public void SetInfo(Action onClickYesButton, string text)
    {
        _onClickYesButton = onClickYesButton;
        _text = text;
        
        RefreshUI();
    }
    
    void RefreshUI()
    {
        if (_init == false)
            return;
    }

    void OnClickYesButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.Sound.Play(Define.Sound.Effect, "Sound_CheckButton");
        _onClickYesButton?.Invoke();
    }

    void OnClickNoButton()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Sound_CancelButton");
        OnComplete();
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
