using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }
    
    public enum Scene
    {
        Unknown,
        Dev,
        Game
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max,
    }

    public enum AnimState
    {
        None,
        Idle,
        Sweat,
        Walking,
        Working,
        Attack,
    }

    public enum JobTitleType
    {
        Intern = 0,     // 사용안함.
        Sinib,          // 주인공 시작
        Daeri,
        Gwajang,
        Bujang,
        Esa,
        Sajang,
        Cat,
    }

    public const int JOB_TITLE_TYPE_COUNT = (int)JobTitleType.Sajang + 1;
    public const int MAX_COLLECTION_COUNT = 100;
    public const int MAX_PROJECT_COUNT = 10;
    public const int MAX_ENDING_COUNT = 10;


    public const int StartButtonText = 19997;
    public const int ContinueButtonText = 19998;
    public const int CollectionButtonText = 19999;

    public const int Sinibe = 20001;
    public const int DataResetConfirm = 20022;

}
