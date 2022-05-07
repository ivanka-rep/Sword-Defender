
namespace SwordDefender.Data.ShopItemsData.Interfaces
{
    public interface IProduct
    {
        string ProductId { get; set; }
        ProductType ProductType { get; set; }
        string Name { get; set; }
        int Price { get; set; }
        string IconId { get; set; }
    }
}

namespace SwordDefender.Data.ShopItemsData
{
    public enum ProductType
    {
        Weapon,
        Skin
    }
}