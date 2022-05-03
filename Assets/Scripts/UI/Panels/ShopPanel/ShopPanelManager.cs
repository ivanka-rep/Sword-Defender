using UnityEngine;
using UnityEngine.UI;

namespace SwordDefender.UI.Panels.Shop
{
    public class ShopPanelManager : PanelBase
    {
        #region Serialized Fields

        [SerializeField] private RectTransform productsContent = null;
        [SerializeField] private ShopItem weaponItem = null;
        [SerializeField] private ShopItem skinItem = null;

        [Space(10)]
        
        [SerializeField] private Toggle weaponToggle = null;
        [SerializeField] private Toggle skinToggle = null;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_onPanelActivated.AddListener(isActivated => { if (isActivated) Init(); });
            
            weaponToggle.onValueChanged.AddListener(isOn => { if (isOn) InstantiateItems(weaponItem);});
            skinToggle.onValueChanged.AddListener(isOn => { if (isOn) InstantiateItems(skinItem);});
        }

        #endregion

        #region Public Methods

        public void OnBackButtonClick() =>
            PanelsManager.Instance.ActivateMenuPanel(this);
        
        #endregion

        #region Private Methods

        private void Init()
        {
            Refresh();
            if (!weaponToggle.isOn) { weaponToggle.isOn = true; }
            else { InstantiateItems(weaponItem); }
        }
        
        private void Refresh()
        {
            foreach (Transform child in productsContent) 
            { Destroy(child.gameObject); }
        }

        private void InstantiateItems(ShopItem item)
        {
            Refresh();
            
            //TODO: manage amount and type of product by config
            for (int i = 0; i < 2; i++)
            { Instantiate(item, productsContent); }
        }
        #endregion
    }
}