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

        public void Init(Action onInitializationEnded)
        {
            m_gameManager = GameManager.Instance;
            m_iconsData = iconsDataScriptable.GetAllIcons();
            m_dataPath = Application.persistentDataPath;
            m_gameData = new DataWrapper();
            
            // if (PlayerPrefs.GetInt("FIRST_START", 1) == 1)
            // {
            //     FirstStartInitialization();
            //     PlayerPrefs.SetInt("FIRST_START", 0);
            // }
            // else
            // {
            //     DeserializeData();
            // }

            //todo try to invoke this OnApplicationQuit
            // DataEventManager.OnUserDataChanged.AddListener(userData => SerializeData());
            // DataEventManager.OnInventoryDataChanged.AddListener(inventoryData => SerializeData());
            
            onInitializationEnded.Invoke();
        }

        #endregion

        #region Private Methods

        private void FirstStartInitialization()
        {
            m_userData = new UserData(10, 10);

            var purchasedProducts = new List<IProduct>
            {
                m_gameManager.Config.GetAllSkinsList()[0],
                m_gameManager.Config.GetAllWeaponsList()[0]
            };
            m_inventoryData = new InventoryData(purchasedProducts);
            m_inventoryData.SetSkin("stickman_default");
            m_inventoryData.SetWeapon("sword_default");
            
            SerializeData();
        }

        private void DeserializeData()
        {
            m_gameData = DataSerializer.GetSerializedData<DataWrapper>(m_dataPath + "/gameData.json");
            ReadGameData();
        }

        private void SerializeData()
        {
            SetGameData();
            DataSerializer.SerializeData(m_gameData, m_dataPath + "/gameData.json");
        }

        private void SetGameData()
        {
            m_gameData.UserData = m_userData;
            m_gameData.InventoryData = m_inventoryData;
        }

        private void ReadGameData()
        {
            m_userData = m_gameData.UserData;
            m_inventoryData = m_gameData.InventoryData;
        }
        #endregion

        #region Unity Methods

        private void OnApplicationQuit() =>
            SerializeData();

        #endregion
    }

    [Serializable] public class DataWrapper
    {
        public UserData UserData { get; set; }
        public InventoryData InventoryData { get; set; }
    }
}