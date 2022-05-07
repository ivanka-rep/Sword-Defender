using System.Collections.Generic;
using Data.ShopItemsData;
using Data.ShopItemsData.Interfaces;

namespace Data
{
    public class InventoryData
    {
        public WeaponProductData InstalledWeapon { get; set; }
        public SkinProductData InstalledSkin { get; set; }
        
        public List<IProduct> PurchasedProducts { get; set; }
    }
}