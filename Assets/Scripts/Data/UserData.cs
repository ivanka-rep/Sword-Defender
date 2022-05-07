using System;

namespace SwordDefender.Data
{
    [Serializable] public class UserData
    {
        public UserData(InventoryData inventoryData, uint soft, uint hard)
        {
            InventoryData = inventoryData;
            SoftCurrency = soft;
            HardCurrency = hard;
        }
        
        public InventoryData InventoryData { get; set; }
        public uint SoftCurrency { get; set; } //Cash
        public uint HardCurrency { get; set; } //Donate cash
    }
}