using SwordDefender.Data.ShopItemsData.Interfaces;

namespace SwordDefender.Data.ShopItemsData
{
    public class WeaponProductData : IWeaponProduct
    {
        public string ProductId { get; set; }
        public ProductType ProductType { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string IconId { get; set; }
        public string Damage { get; set; }
        public string Weight { get; set; }
    }
}