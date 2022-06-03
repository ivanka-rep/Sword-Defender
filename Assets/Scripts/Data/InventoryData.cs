using System;
using System.Collections.Generic;
using SwordDefender.Data.ShopItemsData;
using SwordDefender.Data.ShopItemsData.Interfaces;
using SwordDefender.Data.Extensions;

namespace SwordDefender.Data
{
    [Serializable] public class InventoryData
    {
        public InventoryData(List<IProduct> purchasedProducts)
        {
            PurchasedProducts = purchasedProducts;
        }

        public WeaponProductData InstalledWeapon { get; private set; }
        public SkinProductData InstalledSkin { get; private set; }
        
        public List<IProduct> PurchasedProducts { get; }

        public void AddPurchasedProduct(IProduct product)
        {
            PurchasedProducts.Add(product);
            DataEventManager.SendInventoryDataChanged(this);
        }
        
        public void SetWeapon(string weaponId)
        {
            if (PurchasedProducts.IsProductPurchased(weaponId))
                InstalledWeapon = (WeaponProductData)PurchasedProducts
                    .Find(product => product.ProductId == weaponId);
            
            DataEventManager.SendInventoryDataChanged(this);
        }

        public void SetSkin(string skinId)
        {
            if (PurchasedProducts.IsProductPurchased(skinId))
                InstalledSkin = (SkinProductData)PurchasedProducts
                    .Find(product => product.ProductId == skinId);
            
            DataEventManager.SendInventoryDataChanged(this);
        }
    }
}