
using UnityEngine;

namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
        
#region Level Status

        //DEFINE
        public event System.Action<bool> ONLevelEnd;
        public event System.Action ONLevelStart;
        
        //FUNCS
        public void OnONLevelStart(){
            ONLevelStart?.Invoke();
        }
        public void OnONLevelEnd(bool isSuccess)
        {
            ONLevelEnd?.Invoke(isSuccess);
        }
        
        public event System.Action ONLevelReload;

        public void OnONLevelReload()
        {
            ONLevelReload?.Invoke();
        }
        
        public event System.Action ONLevelComplete;

        public void OnONLevelComplete()
        {
            ONLevelComplete?.Invoke();
        }
        
       

#endregion
        //*********************************************************************
        #region VFX
        //DEFINE
        public event System.Action<Vector3> ONPlayParticleHere;
        
        //FUNCS
        public void OnONPlayParticleHere(Vector3 position)
        {
            ONPlayParticleHere?.Invoke(position);
        }
        #endregion
        //*********************************************************************
        #region Settings

        public event System.Action ONSettingsButtonPressed;

        public void OnONSettingsButtonPressed()
        {
            ONSettingsButtonPressed?.Invoke();
        }

        public event System.Action<bool> ONSettingsPanelOpened;

        public void OnONSettingsPanelOpened(bool isOpen)
        {
            ONSettingsPanelOpened?.Invoke(isOpen);
        }
                
        

        #endregion
        //*********************************************************************
        #region Resource

        

        #endregion
        //*********************************************************************
        #region Input

        public event System.Action<IClickable> OnClick;

        public void ONOnClick(IClickable clickable)
        {
            OnClick?.Invoke(clickable);
        }
        #endregion
        








        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            ONLevelStart= null;
            ONLevelEnd = null;
            ONLevelComplete = null;
            ONLevelReload = null;
            
            
            //VFX
            ONPlayParticleHere = null;
            
            
            //Settings
            ONSettingsButtonPressed = null;
            ONSettingsPanelOpened = null;
            
            
            
            //Resource
            
            
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
