using System;
using System.Collections.Generic;
using SwordDefender.Data;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
   #region Serialized Fields
   [SerializeField] private IconsDataObject iconsDataScriptable = null;

   #endregion
   
   #region Puplic
   public UserData UserData => m_userData;
   public Dictionary<string, Sprite> IconsData => iconsData;

   #endregion
   
   #region Private

   private UserData m_userData = null;
   private Dictionary<string, Sprite> iconsData = null;
   #endregion

   #region Public Methods

   public void Init()
   {
       iconsData = iconsDataScriptable.GetAllIcons();
   }

   #endregion
}
