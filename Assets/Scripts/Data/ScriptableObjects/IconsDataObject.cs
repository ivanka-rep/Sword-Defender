using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "IconsData", menuName = "ScriptableObjects/IconsData", order = 1)]
    public class IconsDataObject : ScriptableObject
    {
        [SerializeField] private List<Sprite> commonIcons = null;
        
        [Header("Shop products icons")]
        [SerializeField] private List<Sprite> weaponIcons = null;
        [SerializeField] private List<Sprite> skinIcons = null;

    }
}