
using UnityEngine;

namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
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
        #region Production
        public event System.Action<ProductType, int> OnProductAmountChange;
        public event System.Action OnProductionQueueChange;
        public event System.Action OnProductionComplete;
        

        public void ONOnProductAmountChange(ProductType type, int amount)
        {
            OnProductAmountChange?.Invoke(type, amount);
        }

        public void ONOnProductionQueueChange()
        {
            OnProductionQueueChange?.Invoke();
        }

        public void ONOnProductionComplete()
        {
            OnProductionComplete?.Invoke();
        }
            

        #endregion
        //*********************************************************************
        #region Input

        public event System.Action<IClickable> OnClick;
        public event System.Action<Building> OnBuildingSelect;

        public void ONOnClick(IClickable clickable)
        {
            OnClick?.Invoke(clickable);
        }

        public void ONOnBuildingSelect(Building building)
        {
            OnBuildingSelect?.Invoke(building);
        }
        #endregion
        








        //remove listeners from all of the events here
        public void NextLevelReset()
        {

            //VFX
            ONPlayParticleHere = null;
            
            
            //Settings
            ONSettingsButtonPressed = null;
            ONSettingsPanelOpened = null;

            //Resource
            OnProductAmountChange = null;

            //Input
            OnClick = null;
            OnBuildingSelect = null;
            
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
