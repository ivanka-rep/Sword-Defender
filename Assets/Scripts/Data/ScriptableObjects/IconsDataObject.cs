using System;
using System.Collections.Generic;
using UnityEngine;

namespace SwordDefender.Data
{
    [CreateAssetMenu(fileName = "IconsData", menuName = "ScriptableObjects/IconsData", order = 1)]
    public class IconsDataObject : ScriptableObject
    {
        [SerializeField] private List<IconData> commonIcons = null;
        
        [Header("Shop products icons")]
        [SerializeField] private List<IconData> weaponIcons = null;
        [SerializeField] private List<IconData> skinIcons = null;

        public Dictionary<string, Sprite> GetAllIcons()
        {
            var iconsDictionary = new Dictionary<string, Sprite>();
            var allIconsList = new List<IconData>();
            allIconsList.AddRange(commonIcons);
            allIconsList.AddRange(weaponIcons);
            allIconsList.AddRange(skinIcons);
            
            allIconsList.ForEach(iconData => iconsDictionary.Add(iconData.id, iconData.sprite));
            return iconsDictionary;
        }
    }

    [Serializable] public class IconData
    {
        public string id;
        public Sprite sprite;
    }
}