
using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
        //*********************************************************************
        #region Settings

        public event System.Action OnOpenSettingsPanel;
        public event System.Action OnCloseSettingsPanel;

        public void ONOnOpenSettingsPanel()
        {
            OnOpenSettingsPanel?.Invoke();
        }

        public void ONOnCloseSettingsPanel()
        {
            OnCloseSettingsPanel?.Invoke();
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

        #region Particle
        public event System.Action<Product,int> OnCallProductParticle;
        public event System.Action<Product,Vector3,int> OnShowParticle;

        public void ONOnCallProductParticle(Product product,int amount)
        {
            OnCallProductParticle?.Invoke(product,amount);
        }
        
        public void ONOnShowParticle(Product product,Vector3 position,int amount)
        {
            OnShowParticle?.Invoke(product,position,amount);
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
            //Settings
            OnOpenSettingsPanel = null;
            OnCloseSettingsPanel = null;

            //Production
            OnProductAmountChange = null;
            OnProductionQueueChange = null;
            OnProductionComplete = null;

            //Input
            OnClick = null;
            OnBuildingSelect = null;

        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
