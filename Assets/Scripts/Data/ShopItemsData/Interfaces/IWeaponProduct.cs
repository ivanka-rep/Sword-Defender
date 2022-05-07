
namespace SwordDefender.Data.ShopItemsData.Interfaces
{
   public interface IWeaponProduct : IProduct
   {
      string Damage { get; set; }
      string Weight { get; set; }
   }
}