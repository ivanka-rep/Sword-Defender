
using UnityEngine.Events;

namespace SwordDefender.Data
{
    public class DataEventManager
    {
        public static readonly UnityEvent<UserData> OnUserDataChanged = new UnityEvent<UserData>();
        public static readonly UnityEvent<InventoryData> OnInventoryDataChanged = new UnityEvent<InventoryData>();
        
        public static void SendUserDataChanged(UserData userData) =>
            OnUserDataChanged.Invoke(userData);

        public static void SendInventoryDataChanged(InventoryData inventoryData) =>
            OnInventoryDataChanged.Invoke(inventoryData);
        
    }
}