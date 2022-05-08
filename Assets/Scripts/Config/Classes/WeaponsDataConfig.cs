using System;
using System.Collections.Generic;
using System.Linq;
using SwordDefender.Data.ShopItemsData;
using UnityEngine;

namespace SwordDefender.Config
{
    [Serializable] public class WeaponsDataConfig
    {
        public string productId;
        public string name;
        public string iconId;
        public int price;
        public string damage;
        public string weight;
    }
}