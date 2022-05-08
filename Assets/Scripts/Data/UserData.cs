using System;

namespace SwordDefender.Data
{
    [Serializable] public class UserData
    {
        public UserData( uint soft, uint hard)
        {
            SoftCurrency = soft;
            HardCurrency = hard;
        }
        
        public uint SoftCurrency { get; set; } //Cash
        public uint HardCurrency { get; set; } //Donate cash
    }
}