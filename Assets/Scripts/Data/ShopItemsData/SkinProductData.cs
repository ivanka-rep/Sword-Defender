using Data.ShopItemsData.Interfaces;

namespace Data.ShopItemsData
{
    public class SkinProductData : ISkinProduct
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string IconId { get; set; }
        public string Health { get; set; }
        public string AttackSpeed { get; set; }
    }
}