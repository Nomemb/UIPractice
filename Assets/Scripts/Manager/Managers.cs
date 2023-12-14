using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instace = null;
    public static Managers Instance { get { return s_instace; } }

    private static ResourceManager s_resourceManager = new ResourceManager();    
    private static UIManager s_uiManager = new UIManager();

    
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }
    public static UIManager UI { get { Init(); return s_uiManager; } }


    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        
    }
}
