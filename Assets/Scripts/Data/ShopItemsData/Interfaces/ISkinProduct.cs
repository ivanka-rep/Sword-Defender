
namespace SwordDefender.Data.ShopItemsData.Interfaces
{
   public interface ISkinProduct : IProduct
   {
      string Health { get; set; }
      string AttackSpeed { get; set; }
   }
}