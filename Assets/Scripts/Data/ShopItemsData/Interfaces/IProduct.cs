
namespace Data.ShopItemsData.Interfaces
{
    public interface IProduct
    {
        string Name { get; set; }
        int Price { get; set; }
        string IconId { get; set; }
    }
}