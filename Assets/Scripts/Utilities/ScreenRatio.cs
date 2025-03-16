using UnityEngine;

namespace Utilities
{
    public static class ScreenRatio 
    {
        public static bool isWideScreen { get; private set; }
        
        public static void Init()
        {
            isWideScreen = Screen.height/Screen.width<1.8f;
        }
    }
}
