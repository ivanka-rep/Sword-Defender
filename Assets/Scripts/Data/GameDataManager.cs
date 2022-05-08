using System;
using System.Collections.Generic;
using Features;
using SwordDefender.Data.ShopItemsData.Interfaces;
using SwordDefender.Game;
using UnityEngine;

namespace SwordDefender.Data
{
    public class GameDataManager : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private IconsDataObject iconsDataScriptable = null;
        
        #endregion

        #region Puplic

        public UserData UserData => m_userData;
        public InventoryData InventoryData => m_inventoryData;
        public Dictionary<string, Sprite> IconsData => m_iconsData;

        #endregion

        #region Private

        private DataWrapper m_gameData = null;
        
        private UserData m_userData = null;
        private InventoryData m_inventoryData = null;
        private Dictionary<string, Sprite> m_iconsData = null;

        private GameManager m_gameManager = null;
        private string m_dataPath = "";
        #endregion

        #region Public Methods

        public void Init()
        {
            m_gameManager = GameManager.Instance;
            m_iconsData = iconsDataScriptable.GetAllIcons();
            m_dataPath = Application.persistentDataPath;
            
            if (PlayerPrefs.GetInt("FIRST_START", 1) == 1)
            {
                FirstStartInitialization();
                PlayerPrefs.SetInt("FIRST_START", 0);
            }
            else
            {
                DeserializeData();
            }

        }

        #endregion

        #region Private Methods

        private void FirstStartInitialization()
        {
            m_userData = new UserData(0, 0);

            var purchasedProducts = new List<IProduct>
            {
                m_gameManager.Config.GetAllSkinsList()[0],
                m_gameManager.Config.GetAllWeaponsList()[0]
            };
            m_inventoryData = new InventoryData(purchasedProducts);
            m_inventoryData.SetSkin("stickman_default");
            m_inventoryData.SetWeapon("sword_default");
        }

        private void DeserializeData()
        {
            m_gameData = DataSerializer.GetSerializedData<DataWrapper>(m_dataPath + "/gameData.json");
            
            m_userData = m_gameData.UserData;
            m_inventoryData = m_gameData.InventoryData;
        }

        private void SerializeData()
        {
            m_gameData.UserData = m_userData;
            m_gameData.InventoryData = m_inventoryData;
            
            DataSerializer.SerializeData(m_gameData, m_dataPath + "/gameData.json");
        }
        #endregion
    }

    [Serializable] public class DataWrapper
    {
        public UserData UserData { get; set; }
        public InventoryData InventoryData { get; set; }
    }
}