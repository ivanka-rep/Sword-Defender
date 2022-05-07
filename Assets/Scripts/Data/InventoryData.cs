using System.Collections.Generic;
using SwordDefender.Data.ShopItemsData;
using SwordDefender.Data.ShopItemsData.Interfaces;
using SwordDefender.Data.Extensions;

namespace SwordDefender.Data
{
    public class InventoryData
    {
        public InventoryData(List<IProduct> purchasedProducts)
        {
            PurchasedProducts = purchasedProducts;
        }

        public WeaponProductData InstalledWeapon { get; private set; }
        public SkinProductData InstalledSkin { get; set; }
        
        public List<IProduct> PurchasedProducts { get; }

        public void AddPurchasedProduct(IProduct product)
        {
            PurchasedProducts.Add(product);
        }
        
        public void SetWeapon(WeaponProductData weapon)
        {
            if (PurchasedProducts.IsProductPurchased(weapon))
                InstalledWeapon = weapon;
        }

        public void SetSkin(SkinProductData skin)
        {
            if (PurchasedProducts.IsProductPurchased(skin))
                InstalledSkin = skin;
        }
    }
}