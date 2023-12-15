using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instace = null;
    public static Managers Instance { get { return s_instace; } }

    private static ResourceManager s_resourceManager = new ResourceManager();    
    private static UIManager s_uiManager = new UIManager();
    private static SoundManager s_soundManager = new SoundManager();

    
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }
    public static UIManager UI { get { Init(); return s_uiManager; } }
    public static SoundManager Sound { get { Init(); return s_soundManager; }
    }

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        
    }
}
