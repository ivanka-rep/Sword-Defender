using System.Collections.Generic;
using System.Linq;
using Data.ShopItemsData.Interfaces;

namespace Data.Extensions
{
    public static class InventoryDataExtensions
    {
        public static bool IsProductPurchased(this IEnumerable<IProduct> purchasedProducts, IProduct product) => 
            purchasedProducts.Any(purchased => purchased.ProductId == product.ProductId);
        
    }
}