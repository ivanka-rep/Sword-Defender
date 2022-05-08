using System;
using System.Collections.Generic;
using System.Linq;
using SwordDefender.Data.ShopItemsData;
using UnityEngine;

namespace SwordDefender.Config
{
    [Serializable] public class SkinsDataConfig
    {
        public string productId;
        public string name;
        public int price;
        public string iconId;
        public string health;
        public string attackSpeed;
    }
}