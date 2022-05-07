using SwordDefender.Data.ShopItemsData.Interfaces;

namespace SwordDefender.Data.ShopItemsData
{
    public class SkinProductData : ISkinProduct
    {
        public string ProductId { get; set; }
        public ProductType ProductType { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string IconId { get; set; }
        public string Health { get; set; }
        public string AttackSpeed { get; set; }
    }
}