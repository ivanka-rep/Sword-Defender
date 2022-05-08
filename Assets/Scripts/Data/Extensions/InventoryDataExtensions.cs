using System.Collections.Generic;
using System.Linq;
using SwordDefender.Data.ShopItemsData.Interfaces;

namespace SwordDefender.Data.Extensions
{
    public static class InventoryDataExtensions
    {
        public static bool IsProductPurchased(this IEnumerable<IProduct> purchasedProducts, string productId) => 
            purchasedProducts.Any(purchased => purchased.ProductId == productId);
        
    }
}